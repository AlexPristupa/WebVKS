using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace LogicCore.Tasking.Scheduler.Conditions
{

	public interface IIntervalCondition
	{
		string Key { get; }

		string Title { get; }

        /// <summary>
        /// Если не достаточно GetNextTime, проверка доп.условий
        /// </summary>
		bool CheckForReady(DateTime? lastRun, DateTime now);

        DateTime GetNextTime(DateTime? lastRun, DateTime now);
    }

    /// <summary>
    /// Постоянно выполняемый интервал, зависит от момента запуска
    /// </summary>
    public interface IRepeatableIntervalCondition : IIntervalCondition
    {
        TimeSpan Interval { get; }
    }

    public static class IntervalConditionExtension
    {
        /// <summary>
        /// получить все пересечения за указанный период
        /// </summary>
        public static IEnumerable<DateTime> GetIntersections(this IEnumerable<IIntervalCondition> conditions1, IEnumerable<IIntervalCondition> conditions2, DateTime from, DateTime to)
        {
            var datesRuns1 = conditions1.GetDatesStarts(from, to);
            var datesRuns2 = conditions2.GetDatesStarts(from, to);
            var result = datesRuns1.Intersect(datesRuns2);
            return result;
        }

        public static IEnumerable<DateTime> GetDatesStarts(this IEnumerable<IIntervalCondition> conditions, DateTime from, DateTime to)
        {
            return conditions.SelectMany(c => c.GetDatesStarts(from, to)).Distinct();
        }

        public static IEnumerable<DateTime> GetDatesStarts(this IIntervalCondition condition, DateTime from, DateTime to)
        {
            DateTime? lastrun = null;
            for (var current = from; current <= to;)
            {
                current = condition.GetNextTime(lastrun, current);
                lastrun = current;
                if (current <= to)
                    yield return current;
            }
        }
    }
}