using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace LogicCore.Tasking.Scheduler.Conditions
{
	public class EveryCondition : IRepeatableIntervalCondition
	{
		private TimeSpan _interval;

		public EveryCondition(TimeSpan interval)
		{
			if (interval.TotalMinutes < 1)
				throw new ArgumentException("Минимальный интервал 1 мин", "interval");
			_interval = interval;
        }

        public TimeSpan Interval
		{
			get { return _interval; }
		}

		#region ITriggerChecker Members

		public bool CheckForReady(DateTime? lastRun, DateTime now)
		{
            return now - (lastRun ?? DateTime.MinValue) >= _interval;
		}

		public string Key
		{
			get { return "every"; }
		}

		public string Title
		{
			get
			{
                var interval = GetInterval(out var prefix);
                var result = string.Format("через: {0}{1}", interval, prefix);
				return result;
			}
		}

		private int GetInterval(out char prefix)
		{
			if (_interval.TotalHours > 1
				&& _interval.TotalHours - (int)_interval.TotalHours <= 0)
			{
				prefix = 'ч';
				return (int)_interval.TotalHours;
			}
			prefix = 'м';
			return (int)_interval.TotalMinutes;
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
                return now + _interval;
            }

            var date = lastRun.Value + _interval;
            if (date >= now)
                return date;

            var diff = (now - date).Ticks / _interval.Ticks;
            date = date.AddTicks(diff * _interval.Ticks);

			if (date >= now)
				return date;
			return date + _interval;
        }
    }
}
