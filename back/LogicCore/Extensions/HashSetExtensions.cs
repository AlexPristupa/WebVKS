using System;
using System.Collections.Generic;
using System.Text;

namespace LogicCore.Extensions
{
    public static class HashSetExtensions
    {
        public static void AddRange<T>(this HashSet<T> source, IEnumerable<T> items)
        {
            foreach(var i in items)
            {
                source.Add(i);
            }
        }
    }
}
