using System;

namespace LogicCore.Tasking.Scheduler.Conditions
{
	public class EveryMonthFormatter : IIntervalConditionFormatter
	{
		public static readonly EveryMonthFormatter Instance = new EveryMonthFormatter();

		public static string ToString(EveryMonthCondition condition, bool includeKey)
		{
			if (includeKey)
			{
                var firstSymb = string.Empty;// condition.Include ? string.Empty : "-";
				return string.Format("{0}{1}:{2}", firstSymb, condition.Key, condition.MonthInterval);
			}
			return string.Format("{0}month", condition.MonthInterval);
		}

		public static EveryMonthCondition TryParse(string data)
		{
			try
			{
				var freq = TimeFunctions.ParseMonthFrequency(data);
				var item = new EveryMonthCondition(freq);
				return item;
			}
			catch
			{
				return null;
			}
		}

		#region ICalendarConditionFormatter

		public string Key
		{
			get { return "every"; }
		}

		public Type ConditionType
		{
			get { return typeof(EveryMonthCondition); }
		}

		IIntervalCondition IIntervalConditionFormatter.TryParse(char prefix, string parameters)
		{
			return TryParse(parameters);
		}

		string IIntervalConditionFormatter.ToString(IIntervalCondition condition, bool includeKey)
		{
			return ToString((EveryMonthCondition) condition, includeKey);
		}

		#endregion
	}
}