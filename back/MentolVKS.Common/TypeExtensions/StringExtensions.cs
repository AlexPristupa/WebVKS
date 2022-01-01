using System;

namespace MentolVKS.Common.TypeExtensions
{
    /// <summary>
    ///     Расширение для строк
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        ///     Сравнение строк
        /// </summary>
        /// <param name="string">Проверяемая строка</param>
        /// <param name="compareTo">Строка для сравнения</param>
        /// <param name="comparison">Правила сравнения</param>
        /// <returns>Результат сравнения</returns>
        public static bool Is(this string @string, string compareTo, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            if (@string == null && compareTo == null) return true;

            return (@string?.Equals(compareTo, comparison)).GetValueOrDefault();
        }

        /// <summary>
        ///     Приведение строки к Camel Case
        /// </summary>
        /// <param name="value">Оригинальная строка</param>
        /// <returns>Строка в формате Camel Case</returns>
        public static string ToCamelCase(this string value)
        {
            if (!string.IsNullOrEmpty(value) && value.Length > 1)
            {
                return char.ToLowerInvariant(value[0]) + value.Substring(1)
                       .Replace(" ", string.Empty).Replace("_", string.Empty);
            }
            return value;
        }

        /// <summary>
        /// Проверяем строку на Base64
        /// </summary>
        /// <param name="string">Проверяемая строка</param>
        /// <returns>Результат на соответствие Base64</returns>
        public static bool IsBase64Encoded(this string @string)
        {
            try
            {
                // If no exception is caught, then it is possibly a base64 encoded string
                var unused = Convert.FromBase64String(@string);
                // The part that checks if the string was properly padded to the
                // correct length was borrowed from d@anish's solution
                return @string.Replace(" ", "").Length % 4 == 0;
            }
            catch
            {
                // If exception is caught, then it is not a base64 encoded string
                return false;
            }
        }

        /// <summary>
        /// Разворачивает строку
        /// </summary>
        /// <param name="input">Входная строка</param>
        public static string GetReverse(this string input)
        {
            var charArray = input.ToCharArray();
            Array.Reverse(charArray);

            return new string(charArray);
        }

        /// <summary>
        /// Конвертация строки в Int64. Если строка пустая, то вернётся ноль.
        /// </summary>
        /// <param name="text">Строка для конвертации.</param>
        /// <returns>Число Int64.</returns>
        public static long ToInt64(this string text) => string.IsNullOrWhiteSpace(text) ? default : long.Parse(text);
    }
}