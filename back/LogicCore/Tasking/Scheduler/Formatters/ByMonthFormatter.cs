using System;
using LogicCore.Extensions;

namespace LogicCore.Tasking.Scheduler.Conditions
{
	public class ByMonthFormatter : IIntervalConditionFormatter
	{
		public static readonly ByMonthFormatter Instance = new ByMonthFormatter();

		public static string ToString(ByMonthCondition condition, bool includeKey)
		{
			var parameters = condition.MonthesSortString;
			if (includeKey)
			{
                var firstSymb = string.Empty;// condition.Include ? string.Empty : "-";
				return string.Format("{0}{1}:{2}", firstSymb, condition.Key, parameters);
			}
			return parameters;
		}

		public static ByMonthCondition TryParse(char prefix, string parameters)
		{
			var monthsSplit = parameters.Split(',');
			var length = monthsSplit.Length;
			if (length == 0) return null;
			if (monthsSplit[0].IsNumeric() == false) return null;

			var monthes = new int[length];
			for (int i = 0; i < length; i++)
			{
				monthes[i] = int.Parse(monthsSplit[i]);
			}
            var item = new ByMonthCondition(monthes);// {Include = prefix != '-'};
			return item;
		}

		#region ICalendarConditionFormatter

		public string Key
		{
			get { return "month"; }
		}

		public Type ConditionType
		{
			get { return typeof(ByMonthCondition); }
		}

		IIntervalCondition IIntervalConditionFormatter.TryParse(char prefix, string parameters)
		{
			return TryParse(prefix, parameters);
		}

		string IIntervalConditionFormatter.ToString(IIntervalCondition condition, bool includeKey)
		{
			return ToString((ByMonthCondition)condition, includeKey);
		}
		#endregion
	}
}