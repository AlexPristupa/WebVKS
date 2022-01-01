using System;
using System.Collections.Generic;
using System.Linq;
using LogicCore.Extensions;

namespace LogicCore.Tasking.Scheduler.Conditions
{
	public class ByMonthCondition : IIntervalCondition
	{
		private readonly HashSet<int> _monthes;

		public ByMonthCondition(params int[] month)
		{
			_monthes = new HashSet<int>(month);
		}

		public IEnumerable<int> Month
		{
			get { return _monthes; }
		}

		public string MonthesSortString
		{
			get
			{
				var monthsSort = _monthes.ToArray();
				Array.Sort(monthsSort);
				return string.Join(",", monthsSort.Select(d => d.ToString()).ToArray());
			}
		}

		#region ITriggerChecker Members

		public bool CheckForReady(DateTime? lastRun, DateTime now)
		{
			return _monthes.Contains(now.Month);
		}

		public string Key
		{
			get { return "month"; }
		}

		public string Title
		{
			get
			{
				var result = "месяцы " + MonthesSortString;
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
				now = lastRun.Value.AddMonths(1);

			for (int i = 0; i < 2; i++)
            {
                var next = GetNextTimeYear(now);
				if (next != null && next >= now)
				{
					return next.Value;
                }
				now = now.StartOfYear().AddYears(1);
            }
            throw new FormatException($"Указан несуществующий день месяца {now}");
		}

        private DateTime? GetNextTimeYear(DateTime now)
        {
			DateTime? next = null;
			foreach (var month in _monthes)
            {
                if (month >= now.Month)
                {
                    var ntime = now.AddMonths(month - now.Month);
					if (next == null || ntime < next)
					{
						next = ntime;
                        if (next == now)
                        {
                            now = now.AddYears(1);
                        }
                    }
                }
            }
            return next;
        }
    }
}
