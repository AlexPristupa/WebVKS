using System;

namespace LogicCore.Tasking.Scheduler.Conditions
{
	public class EveryFormatter : IIntervalConditionFormatter
	{
		public static readonly EveryFormatter Instance = new EveryFormatter();

		public static string ToString(EveryCondition condition, bool includeKey)
		{
			string paramPrefix;
			var interv = GetInterval(condition, out paramPrefix);
			if (includeKey)
			{
                var firstSymb = string.Empty;// condition.Include ? string.Empty : "-";
				return string.Format("{0}{1}:{2}{3}", firstSymb, condition.Key, interv, paramPrefix);
			}
			return string.Format("{0}{1}", interv, paramPrefix);
		}

		private static int GetInterval(EveryCondition condition, out string prefix)
		{
			var interval = condition.Interval;
			if (interval.TotalHours > 1
				&& interval.TotalHours - (int)interval.TotalHours <= 0)
			{
				prefix = "hour";
				return (int)interval.TotalHours;
			}
			prefix = "minute";
			return (int)interval.TotalMinutes;
		}

		public static EveryCondition TryParse(string data)
		{
			try
			{
				var freq = TimeFunctions.ParseTimeFrequency(data);
				var item = new EveryCondition(freq);
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
			get { return typeof(EveryCondition); }
		}

		IIntervalCondition IIntervalConditionFormatter.TryParse(char prefix, string parameters)
		{
			return TryParse(parameters);
		}

		string IIntervalConditionFormatter.ToString(IIntervalCondition condition, bool includeKey)
		{
			return ToString((EveryCondition) condition, includeKey);
		}
		#endregion
	}
}