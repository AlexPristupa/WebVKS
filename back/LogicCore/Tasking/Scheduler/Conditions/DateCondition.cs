using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using LogicCore.Common;

namespace LogicCore.Tasking.Scheduler.Conditions
{
	public class DateCondition : IIntervalCondition
	{
		private Date _date;

		public DateCondition(Date date)
		{
			_date = date;
        }

        public Date Date
		{
			get { return _date; }
		}

		#region ITriggerChecker Members

		public bool CheckForReady(DateTime? lastRun, DateTime now)
		{
			if (lastRun == null)
			{
				return now.Date == _date;
			}
			else
            {
				return now.Date == _date && lastRun < now;
            }
		}

		public string Key
		{
			get { return "date"; }
		}

		public string Title
		{
			get
			{
                var result = Date.ToString("dd.MM.yy");
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
			if (_date < now.Date || (lastRun != null && lastRun.Value.Date == _date))
				return DateTime.MaxValue;
			return _date;
        }
    }
}
