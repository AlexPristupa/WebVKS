using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using LogicCore.Extensions;

namespace LogicCore.Extensions.Enumerable
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable items, Action<T> action)
        {
            foreach (T item in items)
            {
                action.Invoke(item);
            }
        }

        public static object FirstOrDefault(this IEnumerable items)
        {
            var enumerator = items.GetEnumerator();
            try
            {
                if (enumerator.MoveNext())
                {
                    return enumerator.Current;
                }
                return null;
            }
            finally
            {
                (enumerator as IDisposable)?.Dispose();
            }
        }

        public static IEnumerable<T> Select<T>(this IEnumerable items, Func<object, T> selector)
        {
            var enumerator = items.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    yield return selector(enumerator.Current);
                }
            }
            finally
            {
                (enumerator as IDisposable)?.Dispose();
            }
        }

        public static IEnumerable Where(this IEnumerable items, Func<object, bool> where)
        {
            var enumerator = items.GetEnumerator();
            try
            {
                if (enumerator.MoveNext())
                {
                    var item = enumerator.Current;
                    if (where(item))
                        yield return item;
                }
            }
            finally
            {
                (enumerator as IDisposable)?.Dispose();
            }
        }
    }
}
