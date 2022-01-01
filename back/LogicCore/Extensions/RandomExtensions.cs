using LogicCore.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LogicCore.Extensions
{
    public static class RandomExtensions
	{
		public static int Next(this Random rnd,  int min, int max, bool includeMax)
        {
            if (min > max)
            {
                throw new ArgumentOutOfRangeException("min");
            }
			if (max != int.MaxValue)
			{
				max = (includeMax) ? max + 1 : max;
			}

			var result = rnd.Next(min, max);
			return result;
		}

		public static long Next(this Random rnd, long min, long max, bool includeMax)
		{
			if (min > max)
			{
				throw new ArgumentOutOfRangeException("min");
			}
			if (max != long.MaxValue)
			{
				max = (includeMax) ? max + 1 : max;
			}

	        long num = max - min;
	        var result = (long) (rnd.NextDouble() * num) + min;
			return result;
		}

		/// <summary>
        /// Исключая максимальное значение
        /// </summary>
        /// <param name="rnd"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
		public static long Next(this Random rnd, long min, long max)
		{
			var result = rnd.Next(min, max, false);
			return result;
		}

        public static decimal NextDecimal(this Random rnd)
        {
            return rnd.NextDecimal(18, 0);
        }

	    /// <summary>
	    /// Генерирует числовые данные с фиксированной точностью и масштабом.
	    /// Точность p максимум 18, масштаб s в диапазоне от 0 до p
	    /// </summary>
	    /// <param name="rnd"></param>
	    /// <param name="p"> Максимальное количество десятичных разрядов числа (как слева, так и справа от десятичной запятой), которые будут храниться.
	    ///  Точность должна быть значением в диапазоне от 1 до максимум 18. Точность по умолчанию составляет 18.</param>
	    /// 
	    /// <param name="s"> Максимальное количество хранимых десятичных разрядов числа справа от десятичной запятой.
	    ///  Это число отнимается от p для определения максимального количества цифр слева от десятичной запятой.
	    ///  Масштаб может принимать значение от 0 до p. Масштаб может быть указан только совместно с точностью.
	    ///  По умолчанию масштаб принимает значение 0; поэтому s в диапазоне от 0 до p.</param>
	    /// <returns></returns>
	    public static decimal NextDecimal(this Random rnd, byte p, byte s)
	    {
	        if (p < 1 || p > 18)
	            throw new ArgumentException("Точность должна быть значением в диапазоне от 1 до максимум 18", "p");

	        if (s > p)
	            throw new ArgumentException("Масштаб должен принимать значение от 0 до p");

	        var val = (long) Math.Pow(10, p);
	        var result =  rnd.Next(-val + 1, val, false)/(decimal) Math.Pow(10, s);
		    return decimal.Round(result, s);
	    }

        public static decimal NextDecimalMoreZero(this Random rnd, byte p)
        {
           
            var s = 0;
            if (p < 1 || p > 18)
                throw new ArgumentException("Точность должна быть значением в диапазоне от 1 до максимум 18", "p");

            if (s > p)
                throw new ArgumentException("Масштаб должен принимать значение от 0 до p");

            var val = (long)Math.Pow(10, p);
            return rnd.Next(0, val, false) / (decimal)Math.Pow(10, s);
        }

	    public static List<int> RandomValues(this Random rnd, int min, int max, int count)
	    {
	        return rnd.RandomValues(min, max, count, true, true, false);
	    }

	    public static List<int> RandomValues(this Random rnd, int min, int max, int count, bool unique, bool sort, bool includeMax)
	    {
	        var values = rnd.RandomValues((long) min, (long) max, count, unique, sort, includeMax);
	        var castValues = new List<int>(values.Count);
	        castValues.AddRange(values.Select(value => (int) value));
	        return castValues;
	    }

	    public static List<long> RandomValues(this Random rnd, long min, long max, int count)
	    {
	        return rnd.RandomValues(min, max, count, true, true, false);
	    }

        /// <summary>
        /// Последовательность случайных чисел в заданном диапазоне
        /// </summary>
        /// <param name="rnd"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="count"></param>
        /// <param name="unique">Элементы последовательности должны принимать уникальные значения</param>
        /// <param name="sort">Сортировать в порядке возрастания</param>
        /// <param name="includeMax">Включая максимальное значение</param>
        /// <returns></returns>
	    public static List<long> RandomValues(this Random rnd, long min, long max, int count, bool unique, bool sort, bool includeMax)
	    {
	        if (count < 1)
                throw new ArgumentException("Количество генерируемых случайных элементов должно быть больше 0", "count");

	        if (min > max)
	            throw new ArgumentOutOfRangeException("min", "Значение min больше значения max");

	        if (!unique && min == max)
	            return System.Linq.Enumerable.Repeat(min, count).ToList();

            if (max != long.MaxValue)
            {
                max = (includeMax) ? max + 1 : max;
            }

	        //элементы генерируемой последовательности случайных чисел должны быть уникальны
	        if (unique)
	        {
	            long delta = max - min;
	            if (delta < count)
	                throw new ArgumentOutOfRangeException(
	                    string.Format(
	                        "Количество уникальных случайных значений ограничено разницей между максимальным и минимальным значениями: {0}",
	                        delta), "count");
	        }


	        var values = new List<long>(count);

            int i = 0;
	        while (count > i)
	        {
	            long value = rnd.Next(min, max);
	            if (unique && values.Contains(value))
	                continue;
	        
                i++;
	            values.Add(value);
	        }

	        if(sort)
                values.Sort();
	        
            return values;
	    }

	    /// <summary>
		/// Выдать случайный элемент коллекции
		/// </summary>
		public static T RandomItem<T>(this Random rnd, IEnumerable<T> items)
		{
            if(!items.Any())
                throw new ArgumentException("Коллекция не должна быть пустой!", "items");

			var list = items as IList<T>;
			if (list != null)
			{
				var index = rnd.Next(0, list.Count, false);
				return list[index];
			}
			else
			{
				var index = rnd.Next(0, items.Count(), false);
				return items.ElementAt(index);
			}
		}

		/// <summary>
		/// Выдать случайный набор элементов коллекции
		/// </summary>
		public static IEnumerable<T> RandomItems<T>(this Random rnd, IEnumerable<T> items, int count)
		{
			var list = items as IList<T>;
			//var newList = new List<T>(count);
			for (int element = 0; element < count; element++)
			{
				if (list != null)
				{
					var index = rnd.Next(list.Count);
					//newList.Add(list[index]);
					yield return list[index];
				}
				else
				{
					var index = rnd.Next(items.Count());
					//newList.Add(items.ElementAt(index));
					yield return items.ElementAt(index);
				}
			}
			//if (ordered)
			//{
			//	newList.Sort();
			//}
			//return newList;
		}

	    /// <summary>
	    /// Выдать случайный набор неповторяющихся элементов коллекции
	    /// </summary>
	    public static IEnumerable<T> RandomItems<T>(this Random rnd, IEnumerable<T> items, int count, IEnumerable<int> usingIndexes)
	    {
	        var usingIndexesSet = usingIndexes as HashSet<int>;
	            if (usingIndexesSet == null && usingIndexes != null)
	            {
                    usingIndexesSet = new HashSet<int>(usingIndexes);//.ToHashSet();
	            }

	    var list = items as IList<T>;
            //var newList = new List<T>(count);
            for (int element = 0; element < count; element++)
            {
                if (list != null)
                {
                    var index = rnd.Next(list.Count);
                    if (usingIndexesSet != null)
                    {
                        if (list.Count == usingIndexesSet.Count)
                        {
                            // кол-во запрашиваемых уникальных элементов превысило кол-во реальных элементов
                            throw new InvalidDataException("Кол-во уникальных элементов исчерпано!");
                        }

                        //добиваемся уникальности индекса в рамках списка
                        while (usingIndexesSet.Contains(index))
                        {
                            index = rnd.Next(list.Count);
                        }

                        usingIndexesSet.Add(index);
                    }

                    //newList.Add(list[index]);
                    yield return list[index];
                }
                else
                {
                    var index = rnd.Next(items.Count());
                    if (usingIndexesSet != null)
                    {
                        if (items.Count() == usingIndexesSet.Count)
                        {
                            // кол-во запрашиваемых уникальных элементов превысило кол-во реальных элементов
                            throw new InvalidDataException("Кол-во уникальных элементов исчерпано!");
                        }

                        //добиваемся уникальности индекса в рамках списка
                        while (usingIndexesSet.Contains(index))
                        {
                            index = rnd.Next(items.Count());
                        }

                        usingIndexesSet.Add(index);
                    }
                    yield return items.ElementAt(index);
                }
            }
        }

        /// <summary>
		/// Выдать набор элементов коллекции с произвольным шагом смещения, минимально два элемента
		/// </summary>
		/// <param name="rnd"></param>
		/// <param name="items"></param>
		/// <param name="indexStartMax">Максимальный стартовый индекс</param>
		/// <param name="stepMax">Смещение между наборами</param>
		/// <param name="countMax">Максимальное количество полученных шагов</param>
		public static IEnumerable<T> RandomItemsWithStep<T>(this Random rnd, IEnumerable<T> items, int indexStartMax, float stepMax, int countMax)
		{
			if (stepMax < 1)
			{
				throw new ArgumentException("stepMax");
			}
			if (countMax < 2)
			{
				throw new ArgumentException("countMax");
			}

			var count = 0;
			var shift = 0;
			var step = rnd.Next(indexStartMax);
			//var list = items as IList<T>;
			//var newList = new List<T>(count);
			var iterator = items.GetEnumerator();
			T lastItem = default(T);
			T firstItem = default(T);
			while (iterator.MoveNext() && count < countMax)
			{
				if (Equals(firstItem, default(T)))
				{
					firstItem = iterator.Current;
				}
				lastItem = iterator.Current;
				if (++shift >= step)
				{
					step = (int) Math.Round(rnd.Next(100, (int)(stepMax * 100)) / 100f);
					shift = 0;
					count ++;
					yield return iterator.Current;
				}
			}
			if (count < 1)
			{
				yield return firstItem;
			}
			if (count < 2)
			{
				yield return lastItem;
			}
			//if (count < 2)
			//{
			//	throw new InvalidDataException("Выдано менее двух элементов");
			//}
		}

		public static bool NextBoolean(this Random rnd)
		{
			var result = rnd.Next(0, 2) == 0;
			return result;
		}

		/// <summary>
		/// Получить логической значение
		/// </summary>
		/// <param name="rnd"></param>
		/// <param name="percentTrue">Доля истинных значений (0.0 - 1.0)</param>
		public static bool NextBoolean(this Random rnd, float percentTrue)
		{
			var num = rnd.Next(0, 100);
			var result = num < (percentTrue * 100);
			return result;
		}

	    /// <summary>
	    /// Получить случайную дату (включая максимальную дату)
	    /// </summary>
	    /// <param name="rnd"></param>
	    /// <param name="minDate">Минимальная дата</param>
	    /// <param name="maxDate">Максимальная дата</param>
	    /// <exception cref="ArgumentException"></exception>
	    /// <exception cref="ArgumentOutOfRangeException"></exception>
	    public static Date NextDate(this Random rnd, Date minDate, Date maxDate)
	    {
	        if (minDate > maxDate)
	            throw new ArgumentOutOfRangeException("Значение minDate больше значения maxDate");

	        if (minDate == maxDate)
	            return minDate;

            long ticks = rnd.Next(minDate.DateTime.Ticks, maxDate.DateTime.Ticks);
	        var dateTime = new DateTime(ticks);

            return new Date(dateTime);
	    }

		/// <summary>
		/// Получить случайную дату (включая максимальную дату)
		/// </summary>
		/// <param name="rnd"></param>
		/// <param name="currentDate">Текущая дата</param>
		/// <param name="offsetDaysDown">Отклонение дней вниз</param>
		/// <param name="offsetDaysUp">Отклонение дней вверх</param>
		public static Date NextDate(this Random rnd, Date currentDate, int offsetDaysDown, int offsetDaysUp)
		{
			if (offsetDaysDown < 0)
				throw new ArgumentOutOfRangeException("offsetDaysDown");
			if (offsetDaysUp < 0)
				throw new ArgumentOutOfRangeException("offsetDaysUp");

			int days = rnd.Next(currentDate.TotalDays - offsetDaysDown, currentDate.TotalDays + offsetDaysUp);
			var date = new Date(days);
			return date;
		}

		public static Enum NextEnum(this Random rnd, Type enumType)
		{
			var values = EnumExtensions.GetValues(enumType);
			var result = values.GetValue(rnd.Next(0, values.Length));
	        return (Enum) result;
		}

		public static TEntity NextEntity<TEntity>(this Random rnd, IEnumerable<TEntity> entities)
		{
            if(entities == null)
                throw new ArgumentNullException("entities");

			var result = entities.ElementAt(rnd.Next(0, entities.Count() - 1));
			return result;
		}

		public static TimeSpan NextTimeSpan(this Random rnd, int maxHours)
		{
		    if (maxHours == 0)
		        return TimeSpan.Zero;

			var result = new TimeSpan(rnd.Next(maxHours), rnd.Next(60), rnd.Next(60));
			return result;
		}

        public static TimeSpan NextTimeSpan(this Random rnd, TimeSpan maxTimeSpan)
        {
            return rnd.NextTimeSpan(TimeSpan.Zero, maxTimeSpan);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rnd"></param>
        /// <param name="minTimeSpan"></param>
        /// <param name="maxTimeSpan"></param>
        /// <returns></returns>
        public static TimeSpan NextTimeSpan(this Random rnd, TimeSpan minTimeSpan, TimeSpan maxTimeSpan)
        {
            if (minTimeSpan > maxTimeSpan)
                throw new ArgumentOutOfRangeException("Значение minTimeSpan больше значения maxTimeSpan");

            if (minTimeSpan == maxTimeSpan)
                return minTimeSpan;

            long ticks = rnd.Next(minTimeSpan.Ticks, maxTimeSpan.Ticks);
            return new TimeSpan(ticks);
        }

	}
}
