using System;
using System.Collections.Generic;
using System.Linq;

namespace LogicCore.Common.Collection
{
	/// <summary>
	/// Represents a collection of keys and values.
	/// Multiple values can have the same key.
	/// </summary>
	/// <typeparam name="TKey">Type of the keys.</typeparam>
	/// <typeparam name="TValue">Type of the values.</typeparam>
	public class MultiMap<TKey, TValue> : Dictionary<TKey, List<TValue>>, ILookup<TKey, TValue>
    {

		public MultiMap()
			: base()
		{
		}

		public MultiMap(int capacity)
			: base(capacity,  LogicCore.Extensions.EqualityComparer<TKey>.Default)
		{
		}
		public MultiMap(IEqualityComparer<TKey> comparer)
			: base(comparer ?? LogicCore.Extensions.EqualityComparer<TKey>.Default)
		{
		}
		/// <summary>
		/// Adds an element with the specified key and value into the MultiMap. 
		/// </summary>
		/// <param name="key">The key of the element to add.</param>
		/// <param name="value">The value of the element to add.</param>
		public void Add(TKey key, TValue value)
		{
			Add(key, value, false);
		}

		public void Add(TKey key, TValue value, bool checkAndSkipDuplicate)
		{
			List<TValue> valueList;

			if (TryGetValue(key, out valueList))
			{
				if (checkAndSkipDuplicate == false ||
					valueList.Contains(value) == false)
				{
					valueList.Add(value);
				}
			}
			else
			{
				valueList = new List<TValue>();
				valueList.Add(value);
				Add(key, valueList);
			}
		}
		//public void AddRange(IEnumerable<Func<TValue, TKey>> values)
		//{

		//}
        public List<TValue> GetOrCreate(TKey key)
        {
            if (TryGetValue(key, out var values))
            {
                return values;
            }
            var result = new List<TValue>();
            Add(key, result);
            return result;
        }

		/// <summary>
		/// Removes first occurence of an element with a specified key and value.
		/// </summary>
		/// <param name="key">The key of the element to remove.</param>
		/// <param name="value">The value of the element to remove.</param>
		/// <returns>true if the an element is removed;
		/// false if the key or the value were not found.</returns>
		public bool Remove(TKey key, TValue value)
		{
			List<TValue> valueList;

			if (TryGetValue(key, out valueList))
			{
				if (valueList.Remove(value))
				{
					if (valueList.Count == 0)
					{
						Remove(key);
					}
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Removes all occurences of elements with a specified key and value.
		/// </summary>
		/// <param name="key">The key of the elements to remove.</param>
		/// <param name="value">The value of the elements to remove.</param>
		/// <returns>Number of elements removed.</returns>
		public int RemoveAll(TKey key, TValue value)
		{
			List<TValue> valueList;
			int n = 0;

			if (TryGetValue(key, out valueList))
			{
				while (valueList.Remove(value))
				{
					n++;
				}
				if (valueList.Count == 0)
				{
					Remove(key);
				}
			}
			return n;
		}

		/// <summary>
		/// Gets the total number of values contained in the MultiMap.
		/// </summary>
		public int CountAll
		{
			get
			{
				int n = 0;

				foreach (List<TValue> valueList in Values)
				{
					n += valueList.Count;
				}
				return n;
			}
		}

		/// <summary>
		/// Determines whether the MultiMap contains an element with a specific
		/// key / value pair.
		/// </summary>
		/// <param name="key">Key of the element to search for.</param>
		/// <param name="value">Value of the element to search for.</param>
		/// <returns>true if the element was found; otherwise false.</returns>
		public bool Contains(TKey key, TValue value)
		{
			List<TValue> valueList;

			if (TryGetValue(key, out valueList))
			{
				return valueList.Contains(value);
			}
			return false;
		}

		/// <summary>
		/// Determines whether the MultiMap contains an element with a specific value.
		/// </summary>
		/// <param name="value">Value of the element to search for.</param>
		/// <returns>true if the element was found; otherwise false.</returns>
		public bool Contains(TValue value)
		{
			foreach (List<TValue> valueList in Values)
			{
				if (valueList.Contains(value))
				{
					return true;
				}
			}
			return false;
		}

        public void SortSubLists<K>(Func<TValue, K> keySelector)
        {
            foreach (var pair in ((ICollection<KeyValuePair<TKey, List<TValue>>>)this).ToList())
            {
                this[pair.Key] = pair.Value.OrderBy(keySelector).ToList();
            }
        }

        public void SortSubListsDesc<K>(Func<TValue, K> keySelector)
        {
            foreach (var pair in ((ICollection<KeyValuePair<TKey, List<TValue>>>)this).ToList())
            {
                if (pair.Value != null && pair.Value.Count > 1)
                    this[pair.Key] = pair.Value.OrderByDescending(keySelector).ToList();
            }
        }

        public IEnumerable<KeyValuePair<TKey, List<TValue>>> Pairs => this;
        public IEnumerable<IGrouping<TKey, TValue>> Groups => this;

        #region ILookup<TKey,TValue> Members

        bool ILookup<TKey, TValue>.Contains(TKey key)
		{
			return ContainsKey(key);
		}

		IEnumerable<TValue> ILookup<TKey, TValue>.this[TKey key]
		{
			get
			{
				List<TValue> valueList;

				if (TryGetValue(key, out valueList))
					return valueList;
				return Enumerable.Empty<TValue>();
			}
		}

		#endregion

		#region IEnumerable<IGrouping<TKey,TValue>> Members

		IEnumerator<IGrouping<TKey, TValue>> IEnumerable<IGrouping<TKey, TValue>>.GetEnumerator()
		{
			foreach (var pair in this)
			{
				yield return new Grouping(pair);
			}
		}

		class Grouping : IGrouping<TKey, TValue>
		{
			private readonly KeyValuePair<TKey, List<TValue>> _pair;

			public Grouping(KeyValuePair<TKey, List<TValue>> pair)
			{
				_pair = pair;
			}

			#region IGrouping<TKey,TValue> Members

			public TKey Key
			{
				get { return _pair.Key; }
			}

			public IEnumerator<TValue> GetEnumerator()
			{
				return _pair.Value.GetEnumerator();
			}

			System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
			{
				return _pair.Value.GetEnumerator();
			}

			#endregion
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

        //IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        //{
        //    return ((IEnumerable<KeyValuePair<TKey, TValue>>)this).GetEnumerator();
        //}

        #endregion
    }

	public static class MultiMapExtensions
	{

		public static MultiMap<TKey, TSource> ToMultiMap<TKey, TSource>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			return ToMultiMap(source, keySelector, null);
		}

		public static MultiMap<TKey, TSource> ToMultiMap<TKey, TSource, TSort>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TSort> sortSelector)
		{
			var result = ToMultiMap(source, keySelector, null);
			result.SortSubLists(sortSelector);
			return result;
		}

		public static MultiMap<TKey, TSource> ToMultiMap<TKey, TSource>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			var mmap = new MultiMap<TKey, TSource>(comparer);
			foreach (var value in source)
			{
				var key = keySelector(value);
				mmap.Add(key, value);
			}
			return mmap;
		}

        public static MultiMap<TKey, TValue> ToMultiMap<TKey, TSource, TValue, TSort>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> valueSelector, Func<TValue, TSort> sortSelector, IEqualityComparer<TKey> comparer = null)
        {
            var result = ToMultiMap(source, keySelector, valueSelector, comparer);
            result.SortSubLists(sortSelector);
            return result;
        }

        public static MultiMap<TKey, TValue> ToMultiMap<TKey, TSource, TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> valueSelector, IEqualityComparer<TKey> comparer = null)
		{
			var mmap = new MultiMap<TKey, TValue>(comparer);
			foreach (var item in source)
			{
				var key = keySelector(item);
				var value = valueSelector(item);
				mmap.Add(key, value);
			}
			return mmap;
		}
	}
}
