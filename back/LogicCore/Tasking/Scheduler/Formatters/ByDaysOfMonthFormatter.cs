using System;
using System.Linq;
using LogicCore.Extensions;

namespace LogicCore.Tasking.Scheduler.Conditions
{
	public class ByDaysOfMonthFormatter : IIntervalConditionFormatter
	{
		public static readonly ByDaysOfMonthFormatter Instance = new ByDaysOfMonthFormatter();

		public static string ToString(ByDaysOfMonthCondition condition, bool includeKey)
		{
			var parameters = condition.DaysSortString;
			if (includeKey)
			{
                var firstSymb = string.Empty;// condition.Include ? string.Empty : "-";
				return string.Format("{0}{1}:{2}", firstSymb, condition.Key, parameters);
			}
			return parameters;
		}

		public static ByDaysOfMonthCondition TryParse(char prefix, string parameters)
		{
			var daysSplit = parameters.Split(',');
			if (daysSplit.Length == 0) return null;
			var number = daysSplit[0];
			var rangeIndex = number.IndexOf('-');
			if (rangeIndex > 0)
				number = number.Substring(0, rangeIndex);
			if (number.IsNumeric() == false) return null;

			var ranges = RangesParser.FromString(daysSplit).ToArray();
			var item = new ByDaysOfMonthCondition(ranges);
			//item.Include = prefix != '-';
			return item;
		}

		#region ICalendarConditionFormatter

		public string Key
		{
			get { return "days"; }
		}

		public Type ConditionType
		{
			get { return typeof(ByDaysOfMonthCondition); }
		}

		IIntervalCondition IIntervalConditionFormatter.TryParse(char prefix, string parameters)
		{
			return TryParse(prefix, parameters);
		}

		string IIntervalConditionFormatter.ToString(IIntervalCondition condition, bool includeKey)
		{
			return ToString((ByDaysOfMonthCondition)condition, includeKey);
		}
		#endregion
	}
}