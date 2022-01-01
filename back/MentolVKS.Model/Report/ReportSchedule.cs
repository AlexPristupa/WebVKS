using LogicCore.Common;
using LogicCore.Tasking.Scheduler;
using LogicCore.Tasking.Scheduler.Conditions;
using MentolVKS.Model.Enums;
using MentolVKS.Model.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MentolVKS.Model.Report
{
    /// <summary>
    /// Задания
    /// TODO нужно переделать на LogicCore
    /// </summary>
    public class ReportSchedule
    {
        public ReportSchedule(string shedule, DateTime? dateStart, int timeZone = 3)
        {
            Shedule = shedule;
            TimeZone = timeZone;
            DateStart = dateStart;
            if (!DateStart.HasValue)
            {
                DateStart = DateTime.Now;
            }
            try
            {
                condition = SchedulerFormatter.Default.Parse(shedule);
                ParseInterval(shedule);
            }
            catch(Exception ex)
            {
                throw new ValidationErrors(new PropertyError("shedule", $"неверное значение shedule:{Shedule} "+ex.Message));
            }
        }
        
        /// <summary>
        /// Дата старта
        /// </summary>
        private TimeSpan StartTime
        {
            get
            {
                if (DateStart == null) return DateTime.Now.TimeOfDay;

                return DateStart.Value.TimeOfDay;
            }
        } 
        
        private ScheduleConditions condition { get; set; }
        /// <summary>
        /// Значения days:10 и т.д. задача 19545
        /// </summary>
        private string Shedule { get; set; }
        /// <summary>
        /// Часовой пояс
        /// </summary>
        private int TimeZone { get; set; } = 3;
        /// <summary>
        /// Время запуска
        /// </summary>
        private DateTime? DateStart { get; set; } = DateTime.Now;
       
        /// <summary>
        /// Кадендарное число месяца для рассылки
        /// </summary>
        private int OrdinalDay { get; set; } = 1;

        /// <summary>
        /// Тип интервала
        /// </summary>
        private IntervalType IntervalType { get; set; } = IntervalType.OnTime;

        /// <summary>
        /// Выбранные дни недели для отправки отчетов.
        /// </summary>
        private string WeekDays { get; set; }

        /// <summary>
        /// Скалярное значение отправки рассылки: № раз в час|минута
        /// </summary>
        private int TimeUnit { get; set; } = 1;

        /// <summary>
        /// Режим отображения расписания: день, неделя, месяц
        /// </summary>       
        private ScheduleMode ScheduleMode { get; set; } = ScheduleMode.Month;

        /// <summary>
        /// Вид времени периодического выполнения рассылки: час|минута
        /// </summary>
        private ReportTimePart TimePart { get; set; } = ReportTimePart.Hour;

        /// <summary>
        /// Разбирает интервал и заполняет поля модели
        /// </summary>
        /// <param name="interval"></param>
        private void ParseInterval(string interval)
        {
            //var weekReg = new Regex(@"days\:-?\d+", RegexOptions.IgnoreCase);
            if (interval.StartsWith("days"))
            {
                ScheduleMode = ScheduleMode.Week;
                IntervalType = IntervalType.OnTime;

                WeekDays = interval.Replace("days:", string.Empty);

                /*Если число*/
                int day = 0;
                if(int.TryParse(WeekDays, out day)){
                    OrdinalDay = day;
                    ScheduleMode = ScheduleMode.Month;
                    IntervalType = IntervalType.OnTime;
                }
                
                return;
            }
           
            var dayReg = new Regex(@"\d+(day|hour|minute)", RegexOptions.IgnoreCase);
            if (dayReg.IsMatch(interval))
            {
                ScheduleMode = ScheduleMode.Day;

                var daysReg = new Regex(@"\d+day", RegexOptions.IgnoreCase);
                if (daysReg.IsMatch(interval))
                {
                    IntervalType = IntervalType.OnTime;

                    OrdinalDay = Convert.ToInt32(interval.Replace("day", string.Empty));

                    return;
                }

                IntervalType = IntervalType.Period;

                var hourReg = new Regex(@"\d+hour", RegexOptions.IgnoreCase);
                if (hourReg.IsMatch(interval))
                {
                    TimeUnit = Convert.ToInt32(interval.Replace("hour", string.Empty));
                    TimePart = ReportTimePart.Hour;

                    return;
                }

                TimeUnit = Convert.ToInt32(interval.Replace("minute", string.Empty));
                TimePart = ReportTimePart.Minute;

                return;
            }
        }

        /// <summary>
        /// Периодичность выполнения
        /// </summary>
        public string IntervalFormat
        {
            get
            {
                var result = "Выполняется ";

                switch (ScheduleMode)
                {
                    case ScheduleMode.Day:

                        if (IntervalType == IntervalType.OnTime)
                        {
                            if (OrdinalDay == 1) { result += $"каждый день в #date#"; return result; }
                            if (OrdinalDay >= 2 && OrdinalDay <= 4) { result += $"каждые {OrdinalDay} дня в #date#"; return result; }
                            if (OrdinalDay >= 5 && OrdinalDay <= 20) { result += $"каждые {OrdinalDay} дней в #date#"; return result; }
                            if (OrdinalDay == 21) { result += $"каждый 21 день в #date#"; return result; }
                            if (OrdinalDay == 22 || OrdinalDay == 23) { result += $"каждые {OrdinalDay} дня в #date#"; return result; }
                            
                            result += $"каждые {OrdinalDay} дней в #date#"; return result;
                        }
                        else
                        {
                            if (TimePart == ReportTimePart.Hour)
                            {
                                result += GetEveryHours();
                            }
                            else
                            {
                                result += GetEveryMinutes();
                            }
                        }

                        break;
                    case ScheduleMode.Week:
                        var last = new Regex(@"-1+(monday|tuesday|wednesday|thursday|friday|saturday|sunday)", RegexOptions.IgnoreCase);
                        var first = new Regex(@"(1|2|3|4|5)+(monday|tuesday|wednesday|thursday|friday|saturday|sunday)", RegexOptions.IgnoreCase);
                        if (last.IsMatch(WeekDays))
                        {
                            WeekDays = WeekDays.Replace("-1", String.Empty);
                            switch (WeekDays.ToLower())
                            {
                                case "monday": result += $"в последний Понедельник месяца в #date#"; return result; 
                                case "tuesday": result += $"в последний Вторник месяца в #date#"; return result;
                                case "wednesday": result += $"в последнюю Среду  месяца в #date#"; return result;
                                case "thursday": result += $"в последний Четверг месяца в #date#"; return result;
                                case "friday": result += $"в последнюю Пятницу месяца в #date#"; return result;
                                case "saturday": result += $"в последнюю Субботу месяца в #date#"; return result;
                                case "sunday": result += $"в последнее Воскресенье месяца в #date#"; return result;
                            }
                        }
                        if (first.IsMatch(WeekDays))
                        {
                            int day = int.Parse(WeekDays.Substring(0, 1));
                            WeekDays = WeekDays.Substring(1, WeekDays.Length - 1);
                            switch (day)
                            {
                                case 1:
                                    switch (WeekDays.ToLower())
                                    {
                                        case "monday": result += $"в первый Понедельник месяца в #date#"; break;
                                        case "tuesday": result += $"в первый Вторник месяца в #date#"; break;
                                        case "wednesday": result += $"в первую Среду  месяца в #date#"; break;
                                        case "thursday": result += $"в первый Четверг месяца в #date#"; break;
                                        case "friday": result += $"в первую Пятницу месяца в #date#"; break;
                                        case "saturday": result += $"в первую Субботу месяца в #date#"; break;
                                        case "sunday": result += $"в первое Воскресенье месяца в #date#"; break;
                                    }
                                    return result;
                                case 2:
                                    switch (WeekDays.ToLower())
                                    {
                                        case "monday": result += $"во второй Понедельник месяца в #date#"; break;
                                        case "tuesday": result += $"во второй Вторник месяца в #date#"; break;
                                        case "wednesday": result += $"во вторую Среду  месяца в #date#"; break;
                                        case "thursday": result += $"во второй Четверг месяца в #date#"; break;
                                        case "friday": result += $"во вторую Пятницу месяца в #date#"; break;
                                        case "saturday": result += $"во вторую Субботу месяца в #date#"; break;
                                        case "sunday": result += $"во второе Воскресенье месяца в #date#"; break;
                                    }
                                    return result;
                                case 3:
                                    switch (WeekDays.ToLower())
                                    {
                                        case "monday": result += $"в третий Понедельник месяца в #date#"; break;
                                        case "tuesday": result += $"в третий Вторник месяца в #date#"; break;
                                        case "wednesday": result += $"в третью Среду  месяца в #date#"; break;
                                        case "thursday": result += $"в третий Четверг месяца в #date#"; break;
                                        case "friday": result += $"в третью Пятницу месяца в #date#"; break;
                                        case "saturday": result += $"в третью Субботу месяца в #date#"; break;
                                        case "sunday": result += $"в третье Воскресенье месяца в #date#"; break;
                                    }
                                    return result;
                                case 4:
                                    switch (WeekDays.ToLower())
                                    {
                                        case "monday": result += $"в четвертый Понедельник месяца в #date#"; break;
                                        case "tuesday": result += $"в четвертый Вторник месяца в #date#"; break;
                                        case "wednesday": result += $"в четвертую Среду  месяца в #date#"; break;
                                        case "thursday": result += $"в четвертый Четверг месяца в #date#"; break;
                                        case "friday": result += $"в четвертую Пятницу месяца в #date#"; break;
                                        case "saturday": result += $"в четвертую Субботу месяца в #date#"; break;
                                        case "sunday": result += $"в четвертое Воскресенье месяца в #date#"; break;
                                    }
                                    return result;
                                case 5:
                                    switch (WeekDays.ToLower())
                                    {
                                        case "monday": result += $"в пятый Понедельник месяца в #date#"; break;
                                        case "tuesday": result += $"в пятый Вторник месяца в #date#"; break;
                                        case "wednesday": result += $"в пятую Среду  месяца в #date#"; break;
                                        case "thursday": result += $"в пятый Четверг месяца в #date#"; break;
                                        case "friday": result += $"в пятую Пятницу месяца в #date#"; break;
                                        case "saturday": result += $"в пятую Субботу месяца в #date#"; break;
                                        case "sunday": result += $"в пятое Воскресенье месяца в #date#"; break;
                                    }
                                    return result;
                            }
                        }

                        //Если просто неделя или список недель.
                        var weeks = new Regex(@"(monday|tuesday|wednesday|thursday|friday|saturday|sunday)", RegexOptions.IgnoreCase);
                        if (weeks.IsMatch(WeekDays))
                        {
                            result += $"по {GetWeekDays()} в #date#";
                        }
                        else
                        {
                            if (WeekDays.Contains(","))
                            {
                                var array = WeekDays.Split(",").ToList();
                                result += $"каждые {String.Join(",", array.GetRange(0, array.Count - 1))} и {array.Last()} числа в #date#";
                            }
                        }

                        if (WeekDays.Contains("-"))
                        {
                            var array = WeekDays.Split("-");
                            if (array.Length == 2)
                            {
                                int start = 0;
                                int end = 0;
                                int.TryParse(array[0].Trim(), out start);
                                int.TryParse(array[1].Trim(), out end);
                                var list = new List<String>();
                                for(var buf = start; buf <= end; buf++)
                                {
                                    list.Add(buf.ToString());
                                }

                                result += $" каждые {String.Join(",", list.GetRange(0, list.Count - 1))} и {list.Last()} числа в #date#";
                            }
                        }

                        break;
                    case ScheduleMode.Month:
                        if (OrdinalDay == -1 || (OrdinalDay >= 29 && OrdinalDay <= 31))
                        {
                            result += $"в последний день месяца в #date#";
                        }
                        else
                        {
                            result += $"каждый месяц {OrdinalDay} числа в #date#";
                        }

                        break;
                    default:
                        result = string.Empty;
                        break;
                }

                return result;
            }
        }

        /// <summary>
        /// Следующий запуск
        /// </summary>
        /// <returns></returns>
        public DateTime? NextRun()
        {
            if (condition != null)
            {                
                if (DateStart == null)
                {
                    return condition.GetNextTime(DateTime.Now, null);
                }
                condition.DateStart = new Date(DateStart.Value.Year, DateStart.Value.Month, DateStart.Value.Day);
                condition.TimeStart = new TimeSpan(DateStart.Value.Hour, DateStart.Value.Minute, DateStart.Value.Second);

                return condition.GetNextTime(DateTime.Now, null);
            }

            return null;
        }

        /// <summary>
        /// Возвращает значение в минутах в родительном падеже
        /// </summary>
        /// <returns>Значение в родительном падеже</returns>
        private string GetEveryMinutes()
        {
            if(TimeUnit==1) return $"каждую минуту";
            if (TimeUnit > 4 && TimeUnit < 21) return $"каждые {TimeUnit} минут";

            switch (TimeUnit % 10)
            {
                case 1: return $"каждую {TimeUnit} минуту";
                case 2:
                case 3:
                case 4: return $"каждые {TimeUnit} минуты";
                default: return $"каждые {TimeUnit} минут";
            }
        }

        /// <summary>
        /// Возвращает значение в часах в родительном падеже
        /// </summary>
        /// <returns>Значение в родительном падеже</returns>
        private string GetEveryHours()
        {
            if (TimeUnit == 1) return $"каждый час";
            if (TimeUnit > 4 && TimeUnit < 21) return $"каждые {TimeUnit} часов";

            switch (TimeUnit % 10)
            {
                case 1: return $"каждый {TimeUnit} час";
                case 2:
                case 3:
                case 4: return $"каждые {TimeUnit} часа";
                default: return $"каждые {TimeUnit} часов";
            }
        }

        /// <summary>
        /// Возвращает значения дней недели
        /// </summary>
        /// <returns>Значения дней недели</returns>
        private string GetWeekDays()
        {
            var dic = new Dictionary<string, string>
            {
                {"Monday", "Понедельникам"},
                {"Tuesday", "Вторникам"},
                {"Wednesday", "Средам"},
                {"Thursday", "Четвергам"},
                {"Friday", "Пятницам"},
                {"Saturday", "Субботам"},
                {"Sunday", "Воскресеньям"}
            };

            if (WeekDays == null) return string.Empty;

            var days = WeekDays.Split(',');

            return string.Join(",", days.Select(d => dic[d]));
        }

    }
}
