using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicCore.Common;
using LogicCore.Common.Collection;
using LogicCore.Extensions;
using LogicCore.Tasking.Scheduler.Conditions;

namespace LogicCore.Tasking.Scheduler
{
	public class ScheduleConditions
	{
        public ScheduleConditions(IEnumerable<IIntervalCondition> conditons)
        {
            Conditions = conditons.ToArray();
            RepeatableInterval = conditons
                    .OfType<IRepeatableIntervalCondition>()
                    .OrderByDescending(c => c.Interval)
                    .FirstOrDefault()?.Interval;
        }
        public ScheduleConditions(params IIntervalCondition[] conditons) 
            : this((IEnumerable<IIntervalCondition>)conditons)
        {
        }
        public string Name { get; set; }
        public string Key { get; set; }
        public IIntervalCondition[] Conditions { get; }
        //зависимость от момента запуска
        public TimeSpan? RepeatableInterval { get; }

        //повторяется в течении дня
        public bool IsRepeatsPerDay => RepeatableInterval != null && RepeatableInterval.Value < TimeSpan.FromDays(1);

        public TimeSpan? TimeStart { get; set; }
        public Date DateStart { get; set; } = Date.Today;
        public DateTime? DateEnd { get; set; }
        public int RepeatCount { get; set; }

        public ILogger Logger { get; set; }
        /// <summary>
        /// Смещение времени хоста (где выполняется)
        /// </summary>
        public TimeSpan OffsetTimeHost { get; set; }

        /// <summary>
        /// Смещение времени клиента (места для которого проверяется)
        /// </summary>
        public TimeSpan OffsetTimeClient { get; set; }

        /// <summary>
        /// Максимальное отклонение текущего времени от указанного времени запуска
        /// </summary>
        public TimeSpan TimeStartDeviation { get; set; } = TimeSpan.FromDays(1);

        /// <summary>
        /// Ожидаемая продолжительность при наложении задач
        /// </summary>
        public TimeSpan ExpectedDuration { get; set; } = TimeSpan.FromMinutes(5);

        //Подготовка к новому запуску
        public DateTime GetNextTime(DateTime now, DateTime? lastRun, int executionsCount = 0)
        {
            var offset = OffsetTimeClient - OffsetTimeHost;
            now += offset;
            lastRun += offset;

            if (DateEnd != null && now > DateEnd ||
                RepeatCount > 0 && executionsCount >= RepeatCount)
            {
                return DateTime.MaxValue;
            }

            if (now < DateStart) now = DateStart;

            lastRun = PrepareLastRun(now, lastRun, false);
            var nextTime = GetNextTimeForConditions(now, lastRun);
            //если время получилось в прошлом, то смещаемся на сутки
            if (nextTime < now)
                nextTime = GetNextTimeForConditions(now.AddDays(1), lastRun);
            if (RepeatableInterval != null && TimeStart != null)
            {
                var dateTimeStart = DateStart + TimeStart.Value;
                //нужно выровнять время для привязки к времени старта
                if (RepeatableInterval.Value.TotalDays < 1)
                    nextTime = dateTimeStart + (nextTime - dateTimeStart).Truncate(RepeatableInterval.Value);
                else
                    nextTime = nextTime.Date + TimeStart.Value;
                if (nextTime < now) nextTime += RepeatableInterval.Value;
            }

            if (DateEnd != null && nextTime > DateEnd)
            {
                return DateTime.MaxValue;
            }
            return nextTime - offset;
        }

        private DateTime? PrepareLastRun(DateTime now, DateTime? lastRun, bool checkDeviation)
        {
            //время последнего запуска всегда берем с учетом времени старта не зависимо от реального времени пуска
            if (RepeatableInterval != null && TimeStart != null)
            {
                //выравниваем время для интервальных запусков
                if (lastRun == null)
                {

                    if (now.ToDate() != DateStart) 
                    {
                        var dateTimeStart = DateStart + TimeStart.Value;
                        lastRun = dateTimeStart + (now - dateTimeStart).Truncate(RepeatableInterval.Value);
                    }
                    else
                    {
                        lastRun = DateStart.ToDateTime().Add(TimeStart.Value);//.Add(-RepeatableInterval.Value);
                    }
                    //если запуск уже опоздал фиксируем выполнение сегодня
                    if (checkDeviation == false || now - lastRun <= TimeStartDeviation)
                        lastRun -= RepeatableInterval.Value;
                }
            }
            //общее округление в меньшую сторону для выравнивания времени
            //для длинных интервалов, если запуск был уже сегодня но раньле указанного времени
            if (lastRun != null && TimeStart != null)
            {
                var dateTimeStart = DateStart + TimeStart.Value;
                lastRun = dateTimeStart + (lastRun.Value - dateTimeStart).Truncate(RepeatableInterval ?? TimeSpan.FromDays(1));
            }
            //if (RepeatableInterval != null && TimeStart != null &&
            //    RepeatableInterval.Value.TotalDays >= 1 &&
            //    //lastRun.Value.Date == now.Date &&
            //    lastRun.Value.TimeOfDay < TimeStart)
            //{
            //    Logger?.Trace($"Запуск {Name} был {lastRun:HH:mm} раньше установленного времени дня, корректируем установленное время {TimeStart:hh\\:mm}");
            //    lastRun = lastRun.Value.Date.Add(TimeStart.Value).Add(-RepeatableInterval.Value);
            //}
            return lastRun;
        }

        private DateTime GetNextTimeForConditions(DateTime now, DateTime? lastRun)
        {
            var nextTime = DateTime.MaxValue;
            foreach (var condition in Conditions)
            {
                nextTime = DateTimeExtensions.Min(nextTime, condition.GetNextTime(lastRun, now));
            }
            //для фиксированных запусков назначаем время
            if (TimeStart != null && RepeatableInterval == null)
                nextTime = nextTime.Date + TimeStart.Value;
            return nextTime;
        }

        public bool Check(DateTime now, DateTime? lastRun, int executionsCount = 0)
        {
            if (RepeatCount > 0 && executionsCount >= RepeatCount)
                return false;
            
            var offset = OffsetTimeClient - OffsetTimeHost;
            now += offset;
            lastRun += offset;
            if (Logger?.IsTraceEnabled == true)
            {
                Logger?.Trace($"Проверка готовности для '{Name}': now = {now:dd.MM.yy HH:mm}(+{OffsetTimeClient.TotalHours}) " +
                    $"lastRun = {lastRun:dd.MM.yy HH:mm}(+{OffsetTimeClient.TotalHours})");
            }
            if (now > DateStart && (DateEnd == null || now.Date <= DateEnd))
            {
                //раньше указанного времени нельзя
                if (TimeStart != null && now.ToDate() == DateStart && now.TimeOfDay < TimeStart)
                {
                    Logger?.Trace($"{now:HH:mm}(+{OffsetTimeClient.TotalHours}) Время выполнения {TimeStart:hh\\:mm}(+{OffsetTimeClient.TotalHours}) еще не наступило");
                    return false;
                }

                var timeDiff = now.TimeOfDay - TimeStart ?? TimeSpan.Zero;
                if (timeDiff >= TimeSpan.Zero && timeDiff <= TimeStartDeviation || RepeatableInterval != null)
                {
                    lastRun = PrepareLastRun(now, lastRun, true);
                    foreach (var condition in Conditions)
                    {
                        if (condition.CheckForReady(lastRun, now) == false)
                        {
                            Logger?.Trace($"Условие не выполнено: {condition}");
                            return false;
                        }
                    }
                    return true;
                }
            } 
            else
            {
                if (now > DateEnd)
                    Logger?.Trace($"Время действия задачи закончилось c {DateStart:dd.MM.yy} по {DateEnd:dd.MM.yy HH:mm}");
                else
                    Logger?.Trace($"Время действия задачи еще не началось c {DateStart:dd.MM.yy} по {DateEnd:dd.MM.yy HH:mm}");
            }
            return false;
        }

        public IEnumerable<DateTime> GetDatesTimesStarts(DateTime from, DateTime to)
        {
            if (from < DateStart) from = DateStart;
            if (DateEnd != null && to > DateEnd) to = DateEnd.Value;
            DateTime? lastRun = null;
            var executionsTime = 0;
            for (var current = from; current <= to;)
            {
                current = GetNextTime(current, lastRun, executionsTime++);
                lastRun = current;
                if (current <= to)
                    yield return current;
                else
                    break;
            }
        }

        public IEnumerable<DateTime> GetDatesStarts(DateTime from, DateTime to)
        {
            if (from < DateStart) from = DateStart;
            if (DateEnd != null && to > DateEnd) to = DateEnd.Value;
            return Conditions.GetDatesStarts(from, to);
        }

        IEnumerable<DatePointIntersection> GetPointsStarts(DateTime from, DateTime to)
        {
            var durationHours = Math.Ceiling(ExpectedDuration.TotalHours);
            foreach(var date in GetDatesTimesStarts(from, to))
            {
                var pointdate = date.Truncate(TimeSpan.FromHours(1));
                for(int i = 0; i < durationHours; i++)
                {
                    yield return new DatePointIntersection
                    {
                        DateTimeStart = date,
                        DateTimeEnd = date + ExpectedDuration,
                        DateTimePoint = pointdate.AddHours(i),
                    };
                }
            }
        }

        /// <summary>
        /// получить все пересечения за указанный период
        /// </summary>
        public List<DateTime> GetIntersections(IEnumerable<ScheduleConditions> schedules, DateTime from, DateTime to)
        {

            foreach (var schedule in schedules)
            {
                if (schedule == this) continue;
                var result = GetIntersections(schedule, from, to);
                if (result.Any())
                    return result;
            }
            return new List<DateTime>();
        }

        public List<DateTime> GetIntersections(ScheduleConditions schedule, DateTime from, DateTime to)
        {
            if (this.IsRepeatsPerDay == false && schedule.IsRepeatsPerDay == false)
            {
                var timeStart1 = TimeStart;
                var timeEnd1 = timeStart1?.Add(ExpectedDuration);
                var timeStart2 = schedule.TimeStart;
                var timeEnd2 = timeStart2?.Add(schedule.ExpectedDuration);

                if (timeStart1 <= timeEnd2 && timeEnd1 >= timeStart2)
                {
                    //с учетом времени
                    var intersections = GetDatesIntersections(schedule, from, to)
                        .Select(dt => dt.Add(timeStart2 ?? TimeSpan.Zero))
                        .ToList();
                    return intersections;
                }
                return new List<DateTime>();
            }
            else
            {
                var points1 = GetPointsStarts(from, to);
                var points2 = schedule.GetPointsStarts(from, to);
                var intersections = points1.Intersect(points2);
                var result = intersections.Select(inter => inter.DateTimeStart).ToList();
                return result;
            }
        }

        public IEnumerable<DateTime> GetDatesIntersections(ScheduleConditions schedule2, DateTime from, DateTime to)
        {
            var datesRuns1 = GetDatesStarts(from, to);
            var datesRuns2 = schedule2.GetDatesStarts(from, to);
            var result = datesRuns1.Intersect(datesRuns2);
            return result;
        }

        public override string ToString()
        {
            return Name ?? string.Join(",", (object[])Conditions);
        }

        class DatePointIntersection : IEquatable<DatePointIntersection>
        {
            public DateTime DateTimeStart;
            public DateTime DateTimeEnd;
            public DateTime DateTimePoint;

            public override bool Equals(object obj)
            {
                return Equals(obj as DatePointIntersection);
            }

            public bool Equals(DatePointIntersection other)
            {
                return other != null &&
                       DateTimeStart <= other.DateTimeEnd &&
                       DateTimeEnd >= other.DateTimeStart;
            }

            public override int GetHashCode()
            {
                return DateTimePoint.GetHashCode();
            }
        }
    }

}
