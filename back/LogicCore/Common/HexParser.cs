using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogicCore.Common
{
    public static class HexParser
    {
        public static byte[] Parse(string hex)
        {
            hex = hex.Replace(" ", "").Replace("\\x", "");
            var result = Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
            return result;
        }
    }
}
