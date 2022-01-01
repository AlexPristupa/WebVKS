using System;
using System.Collections.Generic;

namespace LogicCore.Extensions
{
    public static class DictionaryExtensions
    {
        public static V Find<K, V>(this IDictionary<K, V> dictionary, K key)
        {
            V result;
            dictionary.TryGetValue(key, out result);
            return result;
        }

        public static V Find<K, V>(this IDictionary<K, V> dictionary, Nullable<K> key) where K : struct
        {
            if (key == null) return default(V);
            V result;
            dictionary.TryGetValue(key.Value, out result);
            return result;
        }

        public static V? FindNullable<K, V>(this IDictionary<K, V> dictionary, K key) where V : struct
        {
            V result;
            if (dictionary.TryGetValue(key, out result))
                return result;
            return null;
        }

        public static V? FindNullable<K, V>(this IDictionary<K, V> dictionary, K? key) where K : struct where V : struct
        {
            if (key == null) return null;
            V result;
            if (dictionary.TryGetValue(key.Value, out result))
                return result;
            return null;
        }

        public static V FindOrNull<K, V>(this IDictionary<K, V> dictionary, K key) where K : class where V : class
        {
            if (key == null) return null;
            V result;
            dictionary.TryGetValue(key, out result);
            return result;
        }

        public static V Set<K, V>(this IDictionary<K, V> dictionary, K key, Func<K, V> created)
        {
            V value;
            if (dictionary.TryGetValue(key, out value))
                return value;
            value = created(key);
            dictionary.Add(key, value);
            return value;
        }

        public static V Set<K, V>(this IDictionary<K, V> dictionary, K key) where V : new()
        {
            V value;
            if (dictionary.TryGetValue(key, out value))
                return value;
            value = new V();
            dictionary.Add(key, value);
            return value;
        }

        public static V Get<K, V>(this IDictionary<K, V> dictionary, K key, string info)
        {
            if (dictionary.TryGetValue(key, out var result))
            {
                return result;
            }
            throw new KeyNotFoundException($"{info} - не найден ключ {key}");
        }

        public static V GetOrCreate<K, V>(this IDictionary<K, V> dictionary, K key, Func<V> valueCreator)
        {
            if (dictionary.TryGetValue(key, out var result))
            {
                return result;
            }
            var value = valueCreator();
            dictionary[key] = value;
            return value;
        }

        public static void AddRange<K, V>(this IDictionary<K, V> dictionary, IDictionary<K, V> items)
        {
            foreach (var item in items)
            {
                dictionary.Add(item.Key, item.Value);
            }
        }

        public static void AddRange<K, V>(this IDictionary<K, V> dictionary, IEnumerable<V> items, Func<V, K> keyFunc)
        {
            foreach (var item in items)
            {
                var key = keyFunc(item);
                dictionary.Add(key, item);
            }
        }

        public static void AddRangeWithIgnore<K, V>(this IDictionary<K, V> dictionary, IEnumerable<V> items, Func<V, K> keyFunc, ILogger logger)
        {
            foreach (var item in items)
            {
                var key = keyFunc(item);
                if (dictionary.ContainsKey(key))
                {
                    logger.Debug("Обнаружен дублирующий элемент: " + key);
                    //logger.Debug(Json.ToString(item));
                    dictionary[key] = item; //оставляем последний
                }
                else
                {
                    dictionary.Add(key, item);
                }
            }
        }

        public static IDictionary<V, K> SwapDictionary<K, V>(this IDictionary<K, V> dictionary)
        {
            return System.Linq.Enumerable.ToDictionary(dictionary, pair => pair.Value, pair => pair.Key);
        }
    }
}
