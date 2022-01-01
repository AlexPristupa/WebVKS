using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace LogicCore.Tasking.Scheduler.Conditions
{
	public class EveryMonthCondition : IRepeatableIntervalCondition
	{
		private readonly int _monthInterval;

        public EveryMonthCondition(int monthInterval)
        {
            _monthInterval = monthInterval;
        }

		public int MonthInterval
		{
			get { return _monthInterval; }
		}

		public TimeSpan Interval
		{
			get { return TimeSpan.FromDays(31); }
		}


		#region ITriggerChecker Members
		public bool TimeDependent => false;

		public bool CheckForReady(DateTime? lastRun, DateTime now)
		{
            return now >= (lastRun ?? DateTime.MinValue).AddMonths(_monthInterval);
        }

        public string Key
		{
			get { return "every"; }
		}

		public string Title
		{
			get
			{
                var result = string.Format($"через: {_monthInterval} мес");
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
			if (lastRun == null)
			{
				return now.AddMonths(_monthInterval);
			}
			var date = lastRun.Value;
            while ((date = date.AddMonths(_monthInterval)) < now) ;
            return date;
        }
    }
}
