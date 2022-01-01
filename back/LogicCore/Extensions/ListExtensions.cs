using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicCore.Extensions
{
    public static class ListExtensions
    {
        public static int IndexOf<T>(this IList<T> items, Func<T, bool> predicate)
        {
            var count = items.Count;
            for (int i = 0; i < count; i++)
            {
                if (predicate(items[i])) return i;
            }
            return -1;
        }

        public static TElement TryGet<TElement>(this IList<TElement> source, int index, TElement defValue)
        {
            if (index >= source.Count)
                return defValue;
            return source[index];
        }
    }
}
