using LogicCore.Extensions.Enumerable;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicCore.Collection
{
    public sealed class DictionaryInfo<TKey, TValue> : IDictionary<TKey, TValue>
    {
        Dictionary<TKey, TValue> _dictionary;
        private Func<TKey, string> _toString;
        private string _info;

        public DictionaryInfo(string info, Func<TKey, string> toString = null, IEqualityComparer<TKey> comparer = null)
        {
            _info = info;
            _dictionary = new Dictionary<TKey, TValue>(comparer ?? Extensions.EqualityComparer<TKey>.Default);
            _toString = toString ?? ToString;
        }

        public DictionaryInfo(IEqualityComparer<TKey> comparer = null)
            : this($"Dictionary<{typeof(TKey).Name}, {typeof(TValue).Name}>", null, comparer)
        {
        }

        public DictionaryInfo(IDictionary<TKey, TValue> dictionary)
            : this($"Dictionary<{typeof(TKey).Name}, {typeof(TValue).Name}>", null, null)
        {
            foreach (var item in dictionary)
            {
                Add(item);
            }
        }

        public string Info => _info;

        public TValue this[TKey key] {
            get
            {
                try
                {
                    return _dictionary[key];
                }
                catch (KeyNotFoundException)
                {
                    throw new KeyNotFoundException($"{_info}: не найден ключ '{_toString(key)}'");
                }
            }
            set => _dictionary[key] = value;
        }

        string ToString(TKey key)
        {
            if (key is IEnumerable list && !(key is string))
            {
                var result = string.Join(",",
                    list.Where(item => item != null)
                    .Select(item => item.ToString()));
                return result;
            }
            else
            {
                return (key == null) ? "<null>" : key.ToString();
            }
        }

        public ICollection<TKey> Keys => _dictionary.Keys;

        public ICollection<TValue> Values => _dictionary.Values;

        public int Count => _dictionary.Count;

        public bool IsReadOnly => false;

        public void Add(TKey key, TValue value)
        {
            try
            {
                _dictionary.Add(key, value);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException($"{_info}: ключ '{_toString(key)}' уже добавлен");
            }
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            try
            {
                ((IDictionary<TKey, TValue>)_dictionary).Add(item);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException($"{_info}: ключ '{_toString(item.Key)}' уже добавлен");
            }
        }

        public void Clear()
        {
            _dictionary.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _dictionary.Contains(item);
        }

        public bool ContainsKey(TKey key)
        {
            return _dictionary.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            ((IDictionary<TKey, TValue>)_dictionary).CopyTo(array, arrayIndex);
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        public Dictionary<TKey, TValue>.Enumerator GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        public bool Remove(TKey key)
        {
            return _dictionary.Remove(key);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return ((IDictionary<TKey, TValue>)_dictionary).Remove(item);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _dictionary.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        public TValue Find(TKey key)
        {
            TValue result;
            _dictionary.TryGetValue(key, out result);
            return result;
        }


    }
}
