using System;
using System.Collections.Generic;
using System.Text;

namespace LogicCore.Extensions
{
    public static class ByteExtensions
    {
        public static bool EndWith(this byte[] data, byte[] end, int dataLength = -1)
        {
            var l = dataLength > -1 ? dataLength : data.Length;
            var el = end.Length;
            if (el == 0 || el > l) return false;
            for(var i = l - el; i < l; i++)
            {
                if (data[i] != end[el - (l - i)])
                    return false;
            }
            return true;
        }
    }
}
