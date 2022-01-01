using System;
using System.Collections.Generic;
using LogicCore.Common;

namespace LogicCore.Extensions
{
	public static class DateExtensions
	{
		/// <summary>
		/// Пересекаются ли переданные интервалы
		/// </summary>
		/// <param name="start1">начало первого интервала</param>
		/// <param name="end1">конец первого интервала</param>
		/// <param name="start2">начало второго интервала</param>
		/// <param name="end2">конец второго интервала</param>
		/// <returns></returns>
		public static bool IsIntersect(Date start1, Date? end1, Date start2, Date? end2)
		{
			#region Validate

			if (start1 > (end1 ?? Date.MaxValue))
				throw new ArgumentException("Начало и конец первого периода не сочетаются между собой");

			if (start2 > (end2 ?? Date.MaxValue))
				throw new ArgumentException("Начало и конец второго периода не сочетаются между собой");

			#endregion

			var endDate1 = end1 ?? Date.MaxValue;
			var endDate2 = end2 ?? Date.MaxValue;

			var intersect = (start2 >= start1 && start2 <= endDate1) ||
							(endDate2 >= start1 && endDate2 <= endDate1) ||
							(start1 >= start2 && start1 <= endDate2) ||
							(endDate1 >= start2 && endDate1 <= endDate2);

			return intersect;
		}

        /// <summary>
        /// Лежит ли дата в указанном периоде
        /// </summary>
        /// <param name="date">рассматриваемая дата</param>
        /// <param name="start">начало периода</param>
        /// <param name="end">конец периода</param>
        /// <returns></returns>
        public static bool InPeriod(this Date date, Date start, Date? end)
		{
			#region Validate

			if (start > (end ?? Date.MaxValue))
				throw new ArgumentException("Начало и конец периода не сочетаются между собой");

			#endregion

			var endDate = end ?? Date.MaxValue;

			var result =
				(start <= date) && (endDate >= date);

			return result;
		}

		/// <summary>
		/// Возвращает последовательность дат от начальной даты до конечной
		/// </summary>
		/// <param name="dateFrom">начальная дата для последовательности</param>
		/// <param name="dateTo">конечная дата для последовательности</param>
		/// <returns>перечислитель последовательности дат</returns>
		public static IEnumerable<Date> GetDatesRange(Date dateFrom, Date dateTo)
		{
			#region Validate

			if (dateFrom > dateTo)
				throw new ArgumentException("Начальная дата не может быть больше конечной");

			#endregion

			var dates = new List<Date>();
			for (var date = dateFrom; date <= dateTo; date = date.AddDays(1))
			{
				dates.Add(date);
			}
			return dates;
		}
	}
}