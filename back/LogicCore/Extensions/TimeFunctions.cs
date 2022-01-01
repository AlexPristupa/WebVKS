using System;

namespace LogicCore
{
    public static class TimeFunctions
    {
        /// <summary>
        /// Получить частоту повтора времени
        /// </summary>
        /// <param name="frequency">строковое значение частоты</param>
        public static TimeSpan ParseTimeFrequency(string frequency)
        {
            try
            {
                frequency = frequency.TrimEnd('s').ToLower();
                if (frequency.EndsWith("minute"))
                {
                    var value = frequency.Substring(0, frequency.Length - 6);
                    if (string.IsNullOrEmpty(value)) return TimeSpan.FromMinutes(1);
                    return TimeSpan.FromMinutes(int.Parse(value));
                }
                else if (frequency.EndsWith("hour"))
                {
                    var value = frequency.Substring(0, frequency.Length - 4);
                    if (string.IsNullOrEmpty(value)) return TimeSpan.FromHours(1);
                    return TimeSpan.FromHours(int.Parse(value));
                }
                else if (frequency.EndsWith("day"))
                {
                    var value = frequency.Substring(0, frequency.Length - 3);
                    if (string.IsNullOrEmpty(value)) return TimeSpan.FromDays(1);
                    return TimeSpan.FromDays(int.Parse(value));
                }
                else if (frequency.EndsWith("second"))
                {
                    var value = frequency.Substring(0, frequency.Length - 6);
                    if (string.IsNullOrEmpty(value)) return TimeSpan.FromSeconds(1);
                    return TimeSpan.FromSeconds(int.Parse(value));
                }
                else if (frequency == "infinite" || frequency == "single")
                {
                    return TimeSpan.MaxValue;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Неизвестный формат " + frequency, "frequency", ex);
            }
            throw new ArgumentException("Неизвестный формат " + frequency, "frequency");
        }

        /// <summary>
        /// Получить частоту повтора месяцев
        /// </summary>
        /// <param name="frequency">строковое значение частоты</param>
        public static int ParseMonthFrequency(string frequency)
        {
            try
            {
                frequency = frequency.TrimEnd('s').TrimEnd('e').ToLower();
                if (frequency.EndsWith("month"))
                {
                    var value = frequency.Substring(0, frequency.Length - 5);
                    if (string.IsNullOrEmpty(value)) return 1;
                    return int.Parse(value);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Неизвестный формат " + frequency, "frequency", ex);
            }
            throw new ArgumentException("Неизвестный формат " + frequency, "frequency");
        }

        /// <summary>
        /// Вычесть диапазон из даты
        /// </summary>
        /// <param name="frequency">строковое значение частоты</param>
        public static DateTime SubstractionRange(this DateTime date, string range)
        {
            try
            {
                if (range.EndsWith("month", StringComparison.OrdinalIgnoreCase))
                {
                    var value = range.Substring(0, range.Length - 5);
                    if (string.IsNullOrEmpty(value)) return date.AddMonths(-1);
                    return date.AddMonths(-int.Parse(value));
                }
                else if (range.EndsWith("day", StringComparison.OrdinalIgnoreCase))
                {
                    var value = range.Substring(0, range.Length - 3);
                    if (string.IsNullOrEmpty(value)) return date.AddDays(-1);
                    return date.AddDays(-int.Parse(value));
                }
                else if (range.EndsWith("hour", StringComparison.OrdinalIgnoreCase))
                {
                    var value = range.Substring(0, range.Length - 4);
                    if (string.IsNullOrEmpty(value)) return date.AddHours(-1);
                    return date.AddHours(-int.Parse(value));
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Неизвестный формат " + range, "range", ex);
            }
            throw new ArgumentException("Неизвестный формат " + range, "range");
        }
    }
}
