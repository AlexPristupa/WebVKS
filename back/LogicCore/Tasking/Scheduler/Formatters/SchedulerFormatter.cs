using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicCore.Common.Collection;
using LogicCore.Extensions;
using LogicCore.Extensions.Enumerable;
using LogicCore.Tasking.Scheduler.Conditions;

namespace LogicCore.Tasking.Scheduler
{

	public class SchedulerFormatter
	{
		public static readonly SchedulerFormatter Default;

		public string DefaultFormatterKey = "every";

		private readonly MultiMap<string, IIntervalConditionFormatter> _formattersByKey = new MultiMap<string, IIntervalConditionFormatter>();

		private readonly Dictionary<Type, IIntervalConditionFormatter> _formattersByCondition;

		static SchedulerFormatter()
		{
			var formatter = new SchedulerFormatter(new IIntervalConditionFormatter[]
			{
				EveryFormatter.Instance,
				EveryMonthFormatter.Instance,
				//EveryFormatter.Instance,
				//ByDaysIntervalFormatter.Instance,
				ByDaysOfMonthFormatter.Instance,
				ByDaysOfWeekFormatter.Instance,
				//ByDaysEvenOddFormatter.Instance,
				ByMonthFormatter.Instance,
				//ByWeekendFormatter.Instance,
				//RepeatFormatter.Instance,
				//TimeFormatter.Instance,
			});
			Default = formatter;
		}

		public SchedulerFormatter(IEnumerable<IIntervalConditionFormatter> formattres)
		{
			if (formattres == null)
			{
				throw new ArgumentNullException("parsers");
			}
			formattres = formattres.AsList();
			_formattersByKey = formattres.ToMultiMap(f => f.Key);
			_formattersByCondition = formattres.ToDictionary(f => f.ConditionType);
		}

		public void Add(IIntervalConditionFormatter formatter)
		{
			_formattersByKey.Add(formatter.Key, formatter);
			_formattersByCondition.Add(formatter.ConditionType, formatter);
		}

		public ScheduleConditions Parse(string data)
        {
			var conditions = ParseConditions(data);
			return new ScheduleConditions(conditions);
        }

		public List<IIntervalCondition> ParseConditions(string data)
		{
			if (string.IsNullOrEmpty(data))
				throw new FormatException("Не задана строка расписания");
			var lines = data.Split(' ');
			var conditions = new List<IIntervalCondition>(lines.Length);
			foreach (var line in lines)
			{
				var condition = ParseCondition(line);
				conditions.Add(condition);
			}
			return conditions;
		}

		private IIntervalCondition ParseCondition(string data)
		{
			char prefix;
			string key;
			string parameters;
			IntervalConditionFormatter.PrepareParse(data, out prefix, out key, out parameters);
			//var include = prefix != '-';
			var condtionParsers = _formattersByKey.Find(key)?.AsList();
			if (condtionParsers == null)
			{
				parameters = key;
				key = DefaultFormatterKey;
				condtionParsers = _formattersByKey.Find(key)?.AsList();
			}
			if (condtionParsers == null || condtionParsers.Any() == false)
				throw new FormatException(string.Format("Cтрока '{0}' содержит некорректный ключ '{1}'", data, key));

			IIntervalCondition condition = null;
			foreach (var formatter in condtionParsers)
			{
				condition = formatter.TryParse(prefix, parameters);
				if (condition != null) break;
			}
			if (condition == null)
				throw new FormatException(string.Format("Cтрока '{0}' содержит некорректные параметры {2} для ключа '{1}'", data, key, data));

			//var result = new ConditionContext(condition, include);
			return condition;
		}

		public string ToString(IEnumerable<IIntervalCondition> conditions)
		{
			var sb = new StringBuilder();
			foreach (var condition in conditions)
			{
				if (sb.Length > 0) sb.Append(' ');
				//var calendarCondition = condition as ICalendarIntervalCondition;
				//if (calendarCondition != null && calendarCondition.Include == false)
				//	sb.Append('-');
				sb.Append(condition.Key);
				var formatter = _formattersByCondition[condition.GetType()];
				var data = formatter.ToString(condition, false);
				if (data != null)
				{
					sb.Append(':');
					sb.Append(data);
				}
			}
			return sb.ToString();
		}
	}
}
