using System;
using LogicCore.Common;

namespace LogicCore.Extensions
{
	public static class DateTimeExtensions
	{
		public static Date ToDate(this DateTime dt)
		{
			return new Date(dt);
		}

        /// <summary>
        /// Округление даты так  
        /// </summary>
        public static DateTime ToSmallDateTime(this DateTime dateTime)
        {
            if (dateTime.Second < 30)
            {
                return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0);
            }
            else
            {
                return (new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0)).AddMinutes(1);
            }
            
        }

        public static DateTime Round(this DateTime dt, TimeSpan interval)
        {
            var ticks = interval.Ticks;
            return new DateTime((long)(Math.Round((double)dt.Ticks / ticks) * ticks));
        }

        public static DateTime RoundToSeconds(this DateTime dt)
        {
            var ticks = TimeSpan.TicksPerSecond;
            return new DateTime((long)(Math.Round((double)dt.Ticks / ticks) * ticks));
        }

        public static DateTime Truncate(this DateTime dt, TimeSpan interval)
        {
            var ticks = interval.Ticks;
            return new DateTime(dt.Ticks / ticks * ticks);
        }

        public static DateTime TruncateToSeconds(this DateTime dt)
        {
            var ticks = TimeSpan.TicksPerSecond;
            return new DateTime(dt.Ticks / ticks * ticks);
        }

        public static DateTime Min(DateTime dt1, DateTime dt2)
        {
            return (dt1 < dt2) ? dt1 : dt2;
        }

        public static DateTime Max(DateTime dt1, DateTime dt2)
        {
            return (dt1 > dt2) ? dt1 : dt2;
        }

        public static DateTime EndOfPreviousDay(this DateTime date)
        {
            return date.Date.AddSeconds(-1);
        }

        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
        public static bool EqualYearMonth(this DateTime a, DateTime b) => a.Year == b.Year && a.Month == b.Month;

        public static DateTime StartOfMonth(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1);
        }

        public static DateTime EndOfMonth(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1).AddMonths(1).AddTicks(-1);
        }

        public static DateTime StartOfYear(this DateTime dt)
        {
            return new DateTime(dt.Year, 1, 1);
        }

        public static DateTime EndOfYear(this DateTime dt)
        {
            return new DateTime(dt.Year, 1, 1).AddYears(1).AddTicks(-1);
        }

        public static int GetNumberDayOfWeek(this DateTime dt)
        {
            var dayweek = dt.DayOfWeek.GetNumberDayOfWeek();
            return dayweek;
        }

        public static int GetNumberDayOfWeek(this DayOfWeek dw)
        {
            var dayweek = (int)dw;
            if (dayweek == 0) dayweek = 7;
            return dayweek;
        }
    }

    public static class TimeSpanExtensions
    {
        public static TimeSpan RoundToSeconds(this TimeSpan dt)
        {
            var ticks = TimeSpan.TicksPerSecond;
            return new TimeSpan((long)(Math.Round((double)dt.Ticks / ticks) * ticks));
        }

        public static TimeSpan Round(this TimeSpan dt, TimeSpan interval)
        {
            var ticks = interval.Ticks;
            return new TimeSpan((long)(Math.Round((double)dt.Ticks / ticks) * ticks));
        }

        public static TimeSpan TruncateToSeconds(this TimeSpan dt)
        {
            var ticks = TimeSpan.TicksPerSecond;
            return new TimeSpan(dt.Ticks / ticks * ticks);
        }

        public static TimeSpan Truncate(this TimeSpan dt, TimeSpan interval)
        {
            var ticks = interval.Ticks;
            return new TimeSpan(dt.Ticks / ticks * ticks);
        }
    }
}
