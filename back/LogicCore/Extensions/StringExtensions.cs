using System;
using System.Collections.Generic;
using System.Linq;

namespace LogicCore.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Взять строку длиной не более указанной
        /// </summary>
        /// <param name="len">Максимальная длина строки</param>
        public static string LengthMax(this string str, int len)
        {
            if (str.Length <= len) return str;
            return str.Substring(0, len);
        }

        public static bool ContainsAny(this string str, params string[] values)
        {
            return values.Contains(str);
        }

        public static string Left(this string str, int length)
        {
            return str.Substring(0, Math.Min(length, str.Length));
        }

        public static string Right(this string str, int length)
        {
            if (str.Length <= length) return str;
            return str.Substring(str.Length - length, length);
        }

        /// <summary>
        /// Взять до указанных символов
        /// </summary>
        public static string TakeUp(this string str, string symbols)
        {
            var index = str.IndexOf(symbols);
            if (index >= 0)
                return str.Substring(0, index);
            return str;
        }

        public static bool IsNumeric(this string str)
        {
            var l = str.Length;
            for (int i = 0; i < l; i++)
            {
                if (i == 0 && str[i] == '-')
                    continue;
                if (str[i] < '0' || str[i] > '9')
                    return false;
            }
            return true;
        }

        public static bool IsDigits(this string str)
        {
            var l = str.Length;
            for (int i = 0; i < l; i++)
            {
                if (str[i] < '0' || str[i] > '9')
                    return false;
            }
            return true;
        }

        public static bool AllChar(this string str, char symbol)
        {
            var l = str.Length;
            for (int i = 0; i < l; i++)
            {
                if (str[i] != symbol)
                    return false;
            }
            return true;
        }

        public static string RemoveSymbols(this string str, IEnumerable<string> symbols)
        {
            foreach(var symb in symbols)
                str = str.Replace(symb, string.Empty);
            return str;
        }

        public static List<string> SplitSections(this string str, string startSeparate, string endSeparate, char trim = ' ')
        {
            var position = -1;
            var list = new List<string>();

            do
            {
                var i = str.IndexOf(startSeparate, position + 1);
                if (i >= 0)
                {
                    i++;
                    var end = str.IndexOf(endSeparate, i);
                    if (end >= 0)
                    {
                        list.Add(str.Substring(i, end - i).Trim(trim));
                    }
                    position = end;
                }
                else
                {
                    break;
                }
            } while (position >= 0);
            return list;
        }

        public static string TrimEnd(this string str, string trim)
        {
            if (str.EndsWith(trim))
                return str.Left(str.Length - trim.Length);
            return str;
        }

        public static bool Contains(this string str, string value, StringComparison comparisonType)
        {
            return str.IndexOf(value, comparisonType) >= 0;
        }

        public static bool Is(this string @string, string compareTo, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            if (@string == null && compareTo == null) return true;

            return (@string?.Equals(compareTo, comparison)).GetValueOrDefault();
        }

        public static string ReverseString(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            var charArray = str.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
