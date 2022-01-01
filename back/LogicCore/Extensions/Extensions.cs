using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogicCore
{

    public static class CharExtensions
    {
        public static bool IsNumeric(this char ch)
        {
            var result = ch >= '0' && ch <= '9';
            return result;
        }
    }

    public static class MathExtensions
    {
        public static DateTime Max(DateTime d1, DateTime d2)
        {
            return (d1 > d2) ? d1 : d2;
        }

        public static DateTime Min(DateTime d1, DateTime d2)
        {
            return (d1 < d2) ? d1 : d2;
        }
    }

    public static class RegexExtensions
    {
        public static bool FullMatch(string value, string mask)
        {
            var match = Regex.Match(value, mask);
            return match.Success && match.Value == value;
        }
    }

    public static class NumberExtenstions
    {
        public static int ParseOrZero(string num)
        {
            if (num != null &&
                int.TryParse(num, out int result))
                return result;
            return 0;
        }
    }

    public static class Int
    {
        public static int? TryParse(string value)
        {
            if (int.TryParse(value, out var result))
            {
                return result;
            }
            return null;
        }
    }

    public static class Long
    {
        public static long? TryParse(string value)
        {
            if (long.TryParse(value, out var result))
            {
                return result;
            }
            return null;
        }
    }
}
