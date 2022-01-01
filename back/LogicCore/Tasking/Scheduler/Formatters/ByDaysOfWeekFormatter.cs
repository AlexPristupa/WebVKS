using System;
using LogicCore.Extensions;

namespace LogicCore.Tasking.Scheduler.Conditions
{
	public class ByDaysOfWeekFormatter : IIntervalConditionFormatter
	{
		public static readonly ByDaysOfWeekFormatter Instance = new ByDaysOfWeekFormatter();

		public static string ToString(ByDaysOfWeekCondition condition, bool includeKey)
		{
			var parameters = condition.DaysString;
			if (includeKey)
			{
                var firstSymb = string.Empty;// condition.Include ? string.Empty : "-";
				return string.Format("{0}{1}:{2}", firstSymb, condition.Key, parameters);
			}
			return parameters;
		}

		public static ByDaysOfWeekCondition TryParse(char prefix, string parameters)
		{
			var daysSplit = parameters.Split(',');
			var length = daysSplit.Length;
			if (length == 0) return null;

			var days = new DayOfWeek[length];
			var numbers = new int[length];
			for (int i = 0; i < length; i++)
			{
				if (daysSplit[i][0].IsNumeric())
                {
					numbers[i] = daysSplit[i][0] - '0';
					daysSplit[i] = daysSplit[i].Right(daysSplit[i].Length - 1);
				}
				else if (daysSplit[i][0] == '-' && daysSplit[i][1] == '1')
                {
					numbers[i] = -1;
					daysSplit[i] = daysSplit[i].Right(daysSplit[i].Length - 2);
				}
				DayOfWeek result;
				if (Enum.TryParse(daysSplit[i], true, out result))
				{
					days[i] = result;
				}
				else
				{
					return null;
				}
			}
            var item = new ByDaysOfWeekCondition(days, numbers);// {Include = prefix != '-'};
			return item;
		}

		#region ICalendarConditionFormatter

		public string Key
		{
			get { return "days"; }
		}

		public Type ConditionType
		{
			get { return typeof(ByDaysOfWeekCondition); }
		}

		IIntervalCondition IIntervalConditionFormatter.TryParse(char prefix, string parameters)
		{
			return TryParse(prefix, parameters);
		}

		string IIntervalConditionFormatter.ToString(IIntervalCondition condition, bool includeKey)
		{
			return ToString((ByDaysOfWeekCondition)condition, includeKey);
		}
		#endregion
	}
}