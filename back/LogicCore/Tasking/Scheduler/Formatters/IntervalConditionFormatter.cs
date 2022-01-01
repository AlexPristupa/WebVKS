using System;

namespace LogicCore.Tasking.Scheduler.Conditions
{


	public interface IIntervalConditionFormatter
	{
		string Key { get; }

		Type ConditionType { get; }

		IIntervalCondition TryParse(char prefix, string parameters);

		string ToString(IIntervalCondition condition, bool includeKey);
	}


	public class IntervalConditionFormatter
	{
		public static void PrepareParse(string data, out char prefix, out string key, out string parameters)
		{
			var shift = 0;
			prefix = char.MinValue;
			if (data.StartsWith("-") || data.StartsWith("+"))
			{
				prefix = data[0];
				shift = 1;
			}
			var splitter = data.IndexOf(':');
			if (splitter == 0)
			{
				throw new FormatException("Некорректный формат строки " + data);
			}
			if (splitter < 0)
			{
				splitter = data.Length;
			}

			key = data.Substring(shift, splitter - shift);
			parameters = null;
			if (splitter != data.Length) //параметров может не быть
			{
				splitter++;
				parameters = data.Substring(splitter, data.Length - splitter);
			}
		}
	}
}