using System;
using System.Collections.Generic;
using System.Linq;
using LogicCore.Extensions;

namespace LogicCore.Tasking.Scheduler.Conditions
{
	public class ByDaysOfMonthCondition : IIntervalCondition
	{
		private readonly HashSet<int> _days;
        private readonly int _maxDays;
        private bool _lastDay;

		public ByDaysOfMonthCondition(params int[] days)
		{
			_days = new HashSet<int>(days);
            _maxDays = days.Any() ? days.Max() : int.MaxValue;
            _lastDay = days.Any(d => d < 0);
		}

		public IEnumerable<int> Days
		{
			get { return _days; }
		}

		public string DaysSortString
		{
			get
			{
				var daysSort = _days.ToArray();
				Array.Sort(daysSort);
				var result = RangesParser.ToString(daysSort, ',');
				return result;
			}
		}

		#region ICalendarCondition Members

		public bool CheckForReady(DateTime? lastRun, DateTime now)
		{
            //если текущий день последний в месяце и включена опция последний день
            // или текущий день меньше указанного дня выполнения то тоже выполняем
            if (now.Day == DateTime.DaysInMonth(now.Year, now.Month) &&
                (_lastDay || now.Day <= _maxDays))
                return true;
			return _days.Contains(now.Day);
		}

		public string Key
		{
			get { return "days"; }
		}

		public string Title
		{
			get
			{
				var result = "дни " + DaysSortString;
				return result;
			}
		}
		#endregion

		public override string ToString()
		{
			return Title;
		}

		public DateTime GetNextTime(DateTime? lastRun, DateTime now)
		{
			if (lastRun != null && lastRun >= now)
				now = lastRun.Value.AddDays(1);

            for (int i = 0; i < 2; i++)
            {
                var next = GetNextTimeMonth(now);
                if (next != null && next >= now)
                {
                    return next.Value;
                }
                now = now.StartOfMonth().AddMonths(1);
            }
            throw new FormatException($"Указан несуществующий день месяца {now}");
		}

        private DateTime? GetNextTimeMonth(DateTime now)
        {
			DateTime? next = null;
			//дни сортированы, нужно проверить только первый подходящий
            foreach (var day in _days)
            {
                if (day >= now.Day)
                {
					//добавляем недостающие дни
                    var ntime = now.AddDays(day - now.Day);
					if (next == null || ntime < next)
					{
						next = ntime;
					}
                }
                else if (day == -1)
                {
                    var monthShift = now.Date == now.EndOfMonth().Date ? 1 : 0;
                    var ntime = now.AddMonths(monthShift).EndOfMonth().Date;
                    if (next == null || ntime < next) next = ntime;
                }
            }
            return next;
        }
    }
}
