using LogicCore.Extensions;
using LogicCore.Tasking.Scheduler;
using LogicCore.Tasking.Scheduler.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LogicCore.Tasking
{
    public class TaskMultiScheduleContext : TaskContext
    {
        public TaskMultiScheduleContext(ITaskingBase task) : base(task)
        {
        }

        protected TaskMultiScheduleContext()
        {
        }

        readonly Dictionary<string, ScheduleInfo> _schedulesInfo = new();

        public ScheduleConditions[] Schedules { get; set; } = Array.Empty<ScheduleConditions>();
        public ScheduleConditions[] ReadySchedules { get; private set; } = Array.Empty<ScheduleConditions>();

        /// <summary>
        /// Максимальное отклонение текущего времени от указанного времени запуска
        /// </summary>
        public TimeSpan TimeStartDeviation { get; set; } = TimeSpan.FromDays(1);

        public bool ManualExecutionCount { get; set; }

        public void SetSchedules(IEnumerable<string> conditions)
        {

            Schedules = conditions.Select(c => SchedulerFormatter.Default.Parse(c)).ToArray();
            //if (Schedules.Length != Tasks.Length)
            //    throw new ArgumentException("Не совпадают количеств задач и расписаний");
        }

        //Подготовка к новому запуску
        protected override DateTime GetNextTimeProcessing(DateTime now)
        {
            var nextTime = DateTime.MaxValue;
            ScheduleConditions activeSchedule = null;
            foreach (var schedule in Schedules)
            {
                var info = GetScheduleInfo(schedule);
                DateTime next = GetNextTimeSchedule(now, schedule, info);
                if (next < nextTime)
                {
                    activeSchedule = schedule;
                    nextTime = next;
                }
                info.NextRun = next; //сразу фиксируем следующее время
            }
            if (nextTime.Date != DateTime.MaxValue.Date)
            {
                Logger.Debug($"Время следующего выполнения: {nextTime:dd.MM.yy HH:mm}(+{activeSchedule?.OffsetTimeHost.TotalHours})");
            }
            else
            {
                Logger.Info($"Дальнейшее выполнение задачи '{Name}' прекращается");
            }
            return nextTime;
        }

        protected virtual DateTime GetNextTimeSchedule(DateTime now, ScheduleConditions schedule, ScheduleInfo info)
        {
            return schedule.GetNextTime(now, info.LastRun ?? info.CurrentRun, info.ExecutionsCount);
        }

        protected virtual ScheduleInfo GetScheduleInfo(ScheduleConditions schedule)
        {
            var result = _schedulesInfo.GetOrCreate(schedule.Key ?? Name,
                        () => new ScheduleInfo { Key = schedule.Key, Name = schedule.Name });
            if (schedule.Key == null || ReadySchedules.Any(s => s.Key == result.Key))
            {
                result.LastRun = LastRun;
                result.CurrentRun = CurrentRun;
                result.NextRun = NextTimeProcessing;
                if (ManualExecutionCount == false)
                    result.ExecutionsCount = ExecutionsCount;
            }
            if (Logger.IsTraceEnabled)
            {
                Logger.Trace($"GetScheduleInfo {Json.ToString(result, null, true)}");
            }
            return result;
        }

        private void CheckLimits(DateTime now, ScheduleConditions schedule, ScheduleInfo info)
        {
            if (schedule.DateEnd < now)
            {
                Logger.Info($"Действие расписания завершено (до {schedule.DateEnd:dd.MM.yy}): {schedule}");
                RemoveSchedule(schedule);
            }
            else if (schedule.RepeatCount > 0 && info.ExecutionsCount >= schedule.RepeatCount)
            {
                Logger.Info($"Количество выполнений расписания завершено ({info.ExecutionsCount}): {schedule}");
                RemoveSchedule(schedule);
            }
        }

        protected override bool ReadyForProcessing(DateTime now)
        {
            if (now >= NextTimeProcessing)
            {
                Logger.Debug("Проверка готовности расписаний");
                var ready = new List<ScheduleConditions>();
                foreach (var schedule in Schedules)
                {
                    var info = GetScheduleInfo(schedule);
                    schedule.TimeStartDeviation = TimeStartDeviation;
                    if (schedule.Check(now, info.LastRun, info.ExecutionsCount))
                    {
                        Logger.Trace($"Готово для выполнения '{schedule}'");
                        ready.Add(schedule);
                    }
                    else
                    {
                        Logger.Trace($"Пропускаем '{schedule}'");
                        CheckLimits(now, schedule, info);
                    }
                }
                ReadySchedules = ready.ToArray();
                Logger.Debug($"Расписаний для выполнения: {ReadySchedules.Length}");
                return ready.Any();
            }
            return false;
        }

        protected virtual void RemoveSchedule(ScheduleConditions schedule)
        {
            Schedules = Schedules.Where(sch => sch != schedule).ToArray();
            _schedulesInfo.Remove(schedule.Key);
        }

        public override void Restore(IEnumerable<ScheduleInfo> infos)
        {
            _schedulesInfo.AddRange(infos, info => info.Key);
            base.Restore(infos);
        }

        public override ScheduleInfo[] GetInfo()
        {
            var info = base.GetInfo();
            var activeSchedules = ReadySchedules;
            //считываем активные параметры
            for (int i = 0; i < activeSchedules.Length && i < info.Length; i++)
            {
                var scheduleInfo = _schedulesInfo.GetOrCreate(activeSchedules[i].Key, 
                    () => new ScheduleInfo { Key = activeSchedules[i].Key, Name = activeSchedules[i].Name });
                var newInfo = info.FirstOrDefault(inf => inf.Key == scheduleInfo.Key);
                if (newInfo != null)
                {
                    scheduleInfo.LastRun = newInfo.LastRun;
                    scheduleInfo.NextRun = newInfo.NextRun;
                    scheduleInfo.CurrentRun = newInfo.CurrentRun;
                    if (ManualExecutionCount == false)
                        scheduleInfo.ExecutionsCount = info[i].ExecutionsCount;
                }
            }

            //возвращаем все что есть
            var result = _schedulesInfo.Values.ToArray();
            return result;
        }

        public ScheduleInfo[] GetCurrentSchedulesInfo()
        {
            var infos = GetInfo();
            var result = ReadySchedules
                .Select(sch => infos.FirstOrDefault(inf => inf.Key == sch.Key) ?? new ScheduleInfo { Key = sch.Key, Name = sch.Name })
                .ToArray();
            return result;
        }

        public DateTime[] GetSchedulesIntersections(DateTime from, DateTime to)
        {
            if (Schedules != null && Schedules.Length > 1)
            {
                var result = Schedules[0].GetIntersections(Schedules, from, to);
                return result.ToArray();
            }
            return Array.Empty<DateTime>();
        }

        protected override void IncrementExecutionCount()
        {
            if (ManualExecutionCount == false)
            {
                base.IncrementExecutionCount();
            }
        }
    }
}
