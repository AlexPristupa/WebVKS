using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicCore.Extensions
{
    public static class QueueExtensions
    {
        public static List<T> Dequeue<T>(this Queue<T> queue, int count)
        {
            var list = new List<T>(count);
            for (int j = 0; j < count && queue.Count > 0; j++)
            {
                var item = queue.Dequeue();
                list.Add(item);
            }
            return list;
        }
    }
}
