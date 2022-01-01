using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using LogicCore.Extensions;
using LogicCore.Common;
using LogicCore.Collection;

namespace LogicCore.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Range<T>(this IEnumerable<T> items, int startIndex, int endIndex)
        {
            int index = 0;
            foreach (var item in items)
            {
                if (index > endIndex) break;
                if (index++ < startIndex) continue;
                yield return item;
            }
        }
#if !NETCOREAPP
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> items)
        {
            var hash = new HashSet<T>();
            foreach (var item in items)
            {
                hash.Add(item);
            }
            return hash;
        }
#endif
        public static bool Empty<T>(this IEnumerable<T> items)
        {
            return items.Any() == false;
        }

		public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
		{
			foreach (var item in items)
			{
				action(item);
			}
		}

		public static HashSet<TResult> ToHashSet<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> func)
		{
			var hash = new HashSet<TResult>();
			foreach (var item in source)
			{
				var value = func(item);
				hash.Add(value);
			}
			return hash;
		}

        public static HashSet<TResult> ToHashSet<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> func, IEqualityComparer<TResult> cmp)
        {
            var hash = new HashSet<TResult>(cmp);
            foreach (var item in source)
            {
                var value = func(item);
                hash.Add(value);
            }
            return hash;
        }


        /// <summary>
        /// Преобразовать или создать HashSet c пропуском дубликатов
        /// </summary>
        public static HashSet<T> AsHashSet<T>(this IEnumerable<T> source)
		{
			var result = source as HashSet<T> ?? new HashSet<T>(source);
			return result;
		}

		public static HashSet<T> AsHashSet<T>(this IEnumerable<T> source, bool ignoreDuplicate)
		{
			var result = source as HashSet<T>;
			if (result == null)
			{
				result = new HashSet<T>();
				foreach (var item in source)
				{
					if (result.Add(item) == false && ignoreDuplicate == false)
						throw new ArgumentException(string.Format("Дублирующий элемент {0} в {1}", item, source));
				}
			}
			return result;
		}

		public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source, bool ignoreDub)
		{
			var result = new HashSet<T>();
			foreach (var item in source)
			{
				if (result.Add(item) == false && ignoreDub == false)
					throw new ArgumentException(string.Format("Дублирующий элемент {0} в {1}", item, source));
			}
			return result;
		}

		public static SortedDictionary<TKey, TSource> ToSortDictionary<TKey, TSource>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			var dic = new SortedDictionary<TKey, TSource>();
			foreach (var item in source)
			{
				dic.Add(keySelector(item), item);
			}
			return dic;
		}

		/// <summary>
		/// Преобразовать к List или создать его
		/// </summary>
		public static IList<T> AsList<T>(this IEnumerable<T> source)
		{
			var result = source as IList<T>;
			if (result != null &&
				source is IEnumerator<T> == false) //совмещенные енумераторы пропускаем
			{
				return result;
			}
			return new List<T>(source);
		}


		/// <summary>
		/// Преобразовать к List или создать его
		/// </summary>
		public static ICollection<T> AsCollection<T>(this IEnumerable<T> source)
		{
			var result = source as ICollection<T>;
			if (result != null &&
				source is IEnumerator<T> == false) //совмещенные енумераторы пропускаем
			{
				return result;
			}
			return new List<T>(source);
		}

		/// <summary>
		/// Преобразовать к List или создать его, вернет null если источник null
		/// </summary>
		public static ICollection<T> AsCollectionOrNull<T>(this IEnumerable<T> source)
		{
			if (source == null) return null;
			var result = source as ICollection<T>;
			if (result != null &&
				source is IEnumerator<T> == false) //совмещенные енумераторы пропускаем
			{
				return result;
			}
			return new List<T>(source);
		}

		/// <summary>
		/// Преобразовать к List или создать его, вернет null если источник null
		/// </summary>
		public static IList<T> AsListOrNull<T>(this IEnumerable<T> source)
		{
			if (source == null) return null;
			var result = source as IList<T>;
			if (result != null &&
				source is IEnumerator<T> == false) //совмещенные енумераторы пропускаем
			{
				return result;
			}
			return new List<T>(source);
		}

        /// <summary>
        /// Преобразовать к List или создать его, вернет пустой лист если источник null
        /// </summary>
        public static IList<T> AsListOrEmpty<T>(this IEnumerable<T> source)
        {
            if (source == null) return new List<T>();
            var result = source as IList<T>;
            if (result != null &&
                source is IEnumerator<T> == false) //совмещенные енумераторы пропускаем
            {
                return result;
            }
            return new List<T>(source);
        }

        /// <summary>
        /// Собирает словарь из пары  ключ-значение
        /// </summary>
        public static Dictionary<K, V> ToDictionary<K, V>(this IEnumerable<KeyValuePair<K, V>> source)
		{
			//считываем количество и создаем словарь сразу нужного размера, т.к. это намного быстрее, чем наполнять его последовательно
			var count = source as ICollection<KeyValuePair<K, V>>;
			var dic = count == null ? new Dictionary<K, V>(16) : new Dictionary<K, V>(count.Count);
			foreach (var pair in source)
			{
				dic.Add(pair.Key, pair.Value);
			}
			return dic;
		}

		/// <summary>
		/// Собирает словарь из пары  ключ-значение
		/// </summary>
		public static Dictionary<K, V> ToDictionary<K, V>(this IEnumerable<IPair<K, V>> source)
		{
			//считываем количество и создаем словарь сразу нужного размера, т.к. это намного быстрее, чем наполнять его последовательно
			var count = source as ICollection<KeyValuePair<K, V>>;
			var dic = count == null ? new Dictionary<K, V>(16) : new Dictionary<K, V>(count.Count);
			foreach (var pair in source)
			{
				dic.Add(pair.First, pair.Second);
			}
			return dic;
		}

        public static DictionaryInfo<TKey, T> ToDictionary<T, TKey>(this IEnumerable<T> items, Func<T, TKey> keySelector, string info, IEqualityComparer<TKey> comparer = null)
        {
            var dic = new DictionaryInfo<TKey, T>(info, null, comparer);
            foreach (var item in items)
            {
                var key = keySelector(item);
                dic.Add(key, item);
            }
            return dic;
        }

        public static DictionaryInfo<TKey, TValue> ToDictionary<T, TKey, TValue>(this IEnumerable<T> items, Func<T, TKey> keySelector, Func<T, TValue> valueSelector, string info, IEqualityComparer<TKey> comparer = null)
        {
            var dic = new DictionaryInfo<TKey, TValue>(info, null, comparer);
            foreach (var item in items)
            {
                var key = keySelector(item);
                var value = valueSelector(item);
                dic.Add(key, value);
            }
            return dic;
        }

        public static SortedList<TKey, TValue> ToSortedList<T, TKey, TValue>(this IEnumerable<T> items, Func<T, TKey> keySelector, Func<T, TValue> valueSelector, IComparer<TKey> comparer = null)
        {
            var dic = new SortedList<TKey, TValue>(comparer);
            foreach (var item in items)
            {
                var key = keySelector(item);
                var value = valueSelector(item);
                dic.Add(key, value);
            }
            return dic;
        }

        public static DictionaryInfo<TKey, T> ToDictionaryInfo<T, TKey>(this IEnumerable<T> items, Func<T, TKey> keySelector)
        {
            var dic = new DictionaryInfo<TKey, T>();
            foreach (var item in items)
            {
                var key = keySelector(item);
                dic.Add(key, item);
            }
            return dic;
        }

        public static DictionaryInfo<TKey, TValue> ToDictionaryInfo<T, TKey, TValue>(this IEnumerable<T> items, Func<T, TKey> keySelector, Func<T, TValue> valueSelector)
        {
            var dic = new DictionaryInfo<TKey, TValue>();
            foreach (var item in items)
            {
                var key = keySelector(item);
                var value = valueSelector(item);
                dic.Add(key, value);
            }
            return dic;
        }

        //для строк всегда сравнение без регистра
        public static DictionaryInfo<K, T> ToDictionaryWithIgnore<T, K>(this IEnumerable<T> items, Func<T, K> keyFunc, ILogger logger, bool ignoreCase = true)
        {
            var cmp = ignoreCase ? ObjectComparerIgnoreCase<K>.GetComparer() : EqualityComparer<K>.Default;
            var dict = new DictionaryInfo<K, T>(cmp);
            foreach (var item in items)
            {
                var key = keyFunc(item);
                if (logger != null)
                {
                    try
                    {
                        dict.Add(key, item);
                    }
                    catch (ArgumentException ex)
                    {
                        logger.Debug($"Обнаружен дублирующий элемент: {key} ({ex.Message})");
                        if (logger.IsTraceEnabled)
                            logger.Trace(Json.ToString(item));
                        dict[key] = item; //оставляем последний
                    }
                } 
                else
                {
                     dict[key] = item; //берем последний
                }
            }
            return dict;
        }

        public static DictionaryInfo<K, V> ToDictionaryWithIgnore<T, K, V>(this IEnumerable<T> items, Func<T, K> keyFunc, Func<T, V> valueSelector, IEqualityComparer<K> comparer, ILogger logger, bool ignoreCase = true)
        {
            if (ignoreCase == false && comparer == null)
            {
                comparer = EqualityComparer<K>.Default;
            }
            var cmp = comparer ?? ObjectComparerIgnoreCase<K>.GetComparer();
            var dict = new DictionaryInfo<K, V>(cmp);
            foreach (var item in items)
            {
                var key = keyFunc(item);
                var value = valueSelector(item);
                try
                {
                    dict.Add(key, value);
                }
                catch (ArgumentException ex)
                {
                    if (logger != null)
                    {
                        logger.Debug($"Обнаружен дублирующий элемент: {key} ({ex.Message})");
                        if (logger.IsTraceEnabled)
                            logger.Trace(Json.ToString(item));
                    }
                    dict[key] = value; //оставляем последний
                }
            }
            return dict;
        }

        //для строк всегда сравнение без регистра
        public static DictionaryInfo<K, V> ToDictionaryWithIgnore<T, K, V>(this IEnumerable<T> items, Func<T, K> keyFunc, Func<T, V> valueSelector, ILogger logger, bool ignoreCase = true)
        {
            return ToDictionaryWithIgnore(items, keyFunc, valueSelector, null, logger, ignoreCase);
        }

        /// <summary>
        /// Преобразует список в набор связанных пар (1-2, 2-3, 3-4, 4-5 и т.д) для анализа связи парных элементов
        /// </summary>
        /// <param name="source"></param>
        /// <param name="useTerminateNode">Добавлять завершающий узел n-[null]</param>
        public static IEnumerable<Pair<T, T>> ToLinkedPairs<T>(this IEnumerable<T> source, bool useTerminateNode)
		{
			var elm1 = default(T);
			var elm2 = default(T);

			var count = 0;
			foreach (var elm in source)
			{
				if (count > 0)
				{
					elm2 = elm;
					yield return new Pair<T, T>(elm1, elm2);
				}
				elm1 = elm;
				count ++;
			}
			//Добавляем завершающий узел, для выявления последнего элемента
			if (useTerminateNode)
			{
				if (count == 1) elm2 = elm1;
				yield return new Pair<T, T>(elm2, default(T));
			}
		}

		/// <summary>
		/// Преобразовать в коллекцию заданного типа
		/// </summary>
		/// <param name="source">Исходная коллекция</param>
		/// <param name="typeCollection">Тип новой коллекции</param>
		/// <param name="convertValue">Конвертор для значения</param>
		/// <returns></returns>
		public static IList ConvertTo<TSource>(this IEnumerable<TSource> source, Type typeCollection, Func<TSource, Type, object> convertValue)
		{
			IList list;
			var argType = typeCollection;
			if (argType.IsArray)
			{
				list = new ArrayList();
			}
			else
			{
				list = (IList)Activator.CreateInstance(argType);
			}
			var elmType = argType.GetEnumerableElementType();
			foreach (var s in source)
			{
				list.Add(convertValue(s, elmType));
			}
			if (argType.IsArray)
			{
				list = ((ArrayList)list).ToArray(elmType);
			}
			return list;
		}

		/// <summary>
		/// Получить набор уникальных значений по заданным признакам
		/// </summary>
		/// <typeparam name="TSource">Тип источника</typeparam>
		/// <typeparam name="TKey">Тип содержащий признаки для сравнения</typeparam>
		/// <param name="source">Источник</param>
		/// <param name="keySelector">Функция по извлечению признаков сравнения</param>
		public static IEnumerable<TSource> Distinct<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			var result = new HashSet<TKey>();
			foreach (var item in source)
			{
				if (result.Add(keySelector(item)))
					yield return item;
			}
		}

        public static IEnumerable<T> Distinct<T, K>(this IEnumerable<T> items, Func<T, K> keyFunc, ILogger logger)
        {
            var hash = new HashSet<K>();
            foreach (var item in items)
            {
                var key = keyFunc(item);
                if (hash.Contains(key))
                {
                    logger.Debug("Пропущен дублирующий элемент: " + key);
                    if (logger.IsTraceEnabled)
                        logger.Trace(Json.ToString(item));
                }
                else
                {
                    hash.Add(key);
                    yield return item;
                }
            }
        }

        /// <summary>
        /// Разделить коллекцию на части с указанным количеством элементов в пакете
        /// </summary>
        /// <typeparam name="TSource">Тип источника</typeparam>
        /// <param name="source">Источник элементов</param>
        /// <param name="packageSize">Максимальный размер пакета</param>
        public static TSource[][] Split<TSource>(this IEnumerable<TSource> source, int packageSize)
		{
			var result = new List<TSource[]>();
			var index = int.MaxValue;
            TSource[] curArray = null;

			foreach (var item in source)
			{
				if (index >= packageSize)
				{
					curArray = new TSource[packageSize];
					result.Add(curArray);
					index = 0;
				}
				curArray[index ++] = item;
			}
			if (index != packageSize && result.Count > 0)
			{
				var lastArray = result.Last();
				Array.Resize(ref lastArray, index);
				result[result.Count - 1] = lastArray;
			}
			return result.ToArray();
		}

        /// <summary>
        /// Разделить коллекцию на указанное количестов частей (с автоматическим подбором количества в пакете)
        /// </summary>
        /// <typeparam name="TSource">Тип источника</typeparam>
        /// <param name="source">Источник элементов</param>
        /// <param name="partCount">Количество необходимых частей</param>
        public static TSource[][] SplitByPart<TSource>(this IEnumerable<TSource> source, int partCount)
        {
            var result = new List<TSource[]>();
            var data = source.ToArray();
            var packageSize = GetPackageSize(data.Length, partCount, 0);
            var curArray = new TSource[packageSize];
            result.Add(curArray);
            var pos = 0;

            for (int i = 0; i < data.Length; i++)
            {
                if (pos >= packageSize)
                {
                    packageSize = GetPackageSize(data.Length, partCount, result.Count);
                    curArray = new TSource[packageSize];
                    result.Add(curArray);
                    pos = 0;
                }
                curArray[pos++] = data[i];
            }
            return result.ToArray();
        }
        private static int GetPackageSize(int length, int partCount, int curCount)
        {
            var packageSize = length / partCount;
            if (length % partCount > curCount) packageSize++;
            return packageSize;
        }

        /// <summary>
        /// Получить набор дублирующих элементов
        /// </summary>
        /// <typeparam name="TSource">Тип источника</typeparam>
        /// <typeparam name="TKey">Тип содержащий признаки для сравнения</typeparam>
        /// <param name="source">Источник</param>
        /// <param name="keySelector">Функция по извлечению признаков сравнения</param>
        public static IEnumerable<TSource> SelectDuplicate<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			var result = new HashSet<TKey>();
			foreach (var item in source)
			{
				if (result.Add(keySelector(item)) == false)
					yield return item;
			}
		}

		/// <summary>
		/// Получить набор дублирующих элементов
		/// </summary>
		/// <typeparam name="TSource">Тип источника</typeparam>
		/// <param name="source">Источник</param>
		public static IEnumerable<TSource> SelectDuplicate<TSource>(this IEnumerable<TSource> source)
		{
			var result = new HashSet<TSource>();
			foreach (var item in source)
			{
				if (result.Add(item) == false)
					yield return item;
			}
		}

        public static T First<T>(this IEnumerable<T> source, Func<T, bool> filter, string info)
        {
            using (var en = source.GetEnumerator())
            {
                while (en.MoveNext())
                {
                    if (filter(en.Current))
                        return en.Current;
                }
            }
            throw new IndexOutOfRangeException($"{info}: Нет элементов по заданному условию");
        }

        public static IEnumerable<T> Except<T, K>(this IEnumerable<T> from, IEnumerable<T> data,  Func<T, K> keySelector)
        {
            var dataKeys = data.ToHashSet(keySelector);
            var result = from.Where(item => dataKeys.Contains(keySelector(item)) == false);
            return result;
        }

        public static void Add<K,V>(this IList<KeyValuePair<K,V>> pairs, K key, V value)
        {
            pairs.Add(new KeyValuePair<K, V>(key, value));
        }
    }
}
