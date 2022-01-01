/*
Copyright 2013 Clay Anderson

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

	http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using System;
using System.Globalization;

namespace LogicCore.Common
{
	[Serializable]
	public struct Date : IComparable, IFormattable, IComparable<Date>, IEquatable<Date>, IConvertible
	{
		private readonly int _days; //можно еще Year (short), Month (byte), Day (byte)
		private const long TicksDay = TimeSpan.TicksPerDay;

		public static readonly Date MaxValue = new Date(DateTime.MaxValue);
		public static readonly Date MinValue = new Date(DateTime.MinValue);

		public DateTime DateTime { get { return new DateTime(_days * TicksDay); } }

		public Date(int year, int month, int day)
		{
			_days = (int)(new DateTime(year, month, day).Ticks / TicksDay);
		}

		public Date(DateTime dateTime)
		{
			_days = (int)(dateTime.Ticks / TicksDay);
			//_days = dateTime.AddHours(-dateTime.Hour).AddMinutes(-dateTime.Minute).AddSeconds(-dateTime.Second).AddMilliseconds(-dateTime.Millisecond);
		}

		public Date(int days)
		{
			_days = days;
		}

		public int TotalDays { get { return _days; } }

		public static TimeSpan operator -(Date d1, Date d2)
		{
			return TimeSpan.FromDays(d1._days - d2._days);
		}

		public static DateTime operator -(Date d, TimeSpan t)
		{
			return new DateTime(d._days * TicksDay - t.Ticks);
		}

		public static bool operator !=(Date d1, Date d2)
		{
			return d1._days != d2._days;
		}

		public static DateTime operator +(Date d, TimeSpan t)
		{
			return new DateTime(d._days * TicksDay + t.Ticks);
		}

		public static bool operator <(Date d1, Date d2)
		{
			return d1._days < d2._days;
		}

		public static bool operator <=(Date d1, Date d2)
		{
			return d1._days <= d2._days;
		}

		public static bool operator ==(Date d1, Date d2)
		{
			return d1._days == d2._days;
		}

		public static bool operator >(Date d1, Date d2)
		{
			return d1._days > d2._days;
		}

		public static bool operator >=(Date d1, Date d2)
		{
			return d1._days >= d2._days;
		}

		public static implicit operator DateTime(Date d)
		{
			return new DateTime(d._days * TicksDay);
		}

		public static explicit operator Date(DateTime d)
		{
			return new Date(d);
		}

		/// <summary>
		/// День месяца
		/// </summary>
		public int Day
		{
			get
			{
				return DateTime.Day;
			}
		}

		public DayOfWeek DayOfWeek
		{
			get
			{
				return DateTime.DayOfWeek;
			}
		}

		public int DayOfYear
		{
			get
			{
				return DateTime.DayOfYear;
			}
		}

		public int Month
		{
			get
			{
				return DateTime.Month;
			}
		}

		public static Date Today
		{
			get
			{
				return new Date(DateTime.Today);
			}
		}

		public int Year
		{
			get
			{
				return DateTime.Year;
			}
		}

		public Date AddDays(int value)
		{
			return new Date(_days + value);// _days.AddDays(value));
		}

		public Date AddMonths(int value)
		{
			return new Date(DateTime.AddMonths(value));
		}

		public Date AddYears(int value)
		{
			return new Date(DateTime.AddYears(value));
		}

		public static int Compare(Date d1, Date d2)
		{
			return d1.CompareTo(d2);
		}

		public int CompareTo(Date value)
		{
			return _days.CompareTo(value._days);
		}

		public int CompareTo(object value)
		{
			return _days.CompareTo(((Date)value)._days);
		}

		public static int DaysInMonth(int year, int month)
		{
			return DateTime.DaysInMonth(year, month);
		}

		public bool Equals(Date value)
		{
			return _days.Equals(value._days);
		}

		public override bool Equals(object value)
		{
			return value is Date && _days.Equals(((Date)value)._days);
		}

		public override int GetHashCode()
		{
			return _days.GetHashCode();
		}

		public static bool Equals(Date d1, Date d2)
		{
			return d1._days.Equals(d2._days);
		}


		public static bool IsLeapYear(int year)
		{
			return DateTime.IsLeapYear(year);
		}

		public static Date Parse(string s)
		{
			return new Date(DateTime.Parse(s));
		}

		public static Date Parse(string s, IFormatProvider provider)
		{
			return new Date(DateTime.Parse(s, provider));
		}

		public static Date Parse(string s, IFormatProvider provider, DateTimeStyles style)
		{
			return new Date(DateTime.Parse(s, provider, style));
		}

		public static Date ParseExact(string s, string format, IFormatProvider provider)
		{
			return new Date(DateTime.ParseExact(s, format, provider));
		}

		public static Date ParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style)
		{
			return new Date(DateTime.ParseExact(s, format, provider, style));
		}

		public static Date ParseExact(string s, string[] formats, IFormatProvider provider, DateTimeStyles style)
		{
			return new Date(DateTime.ParseExact(s, formats, provider, style));
		}

		public TimeSpan Subtract(Date value)
		{
			return this - value;
		}

		public DateTime Subtract(TimeSpan value)
		{
			return this - value;
		}

		public string ToLongString()
		{
			return DateTime.ToLongDateString();
		}

		public string ToShortString()
		{
			return DateTime.ToShortDateString();
		}

		public string ToShortDateString() //для совместимости с DateTime
		{
			return DateTime.ToShortDateString();
		}

		public override string ToString()
		{
			return DateTime.ToShortDateString();
		}

		public string ToString(IFormatProvider provider)
		{
			return DateTime.ToString(provider);
		}

		public string ToString(string format)
		{
			return DateTime.ToString(format);
		}

		public string ToString(string format, IFormatProvider provider)
		{
			return DateTime.ToString(format, provider);
		}

		public static bool TryParse(string s, out Date result)
		{
			DateTime d;
			bool success = DateTime.TryParse(s, out d);
			result = new Date(d);
			return success;
		}

		public static bool TryParse(string s, IFormatProvider provider, DateTimeStyles style, out Date result)
		{
			DateTime d;
			bool success = DateTime.TryParse(s, provider, style, out d);
			result = new Date(d);
			return success;
		}

		public static bool TryParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style, out Date result)
		{
			DateTime d;
			bool success = DateTime.TryParseExact(s, format, provider, style, out d);
			result = new Date(d);
			return success;
		}

		public static bool TryParseExact(string s, string[] formats, IFormatProvider provider, DateTimeStyles style, out Date result)
		{
			DateTime d;
			bool success = DateTime.TryParseExact(s, formats, provider, style, out d);
			result = new Date(d);
			return success;
		}
		public DateTime ToDateTime()
		{
			return new DateTime(_days * TicksDay);
		}

		#region IConvertible Members

		TypeCode IConvertible.GetTypeCode()
		{
			return TypeCode.DateTime;
		}

		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		byte IConvertible.ToByte(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			return ToDateTime();
		}

		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		double IConvertible.ToDouble(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		short IConvertible.ToInt16(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		int IConvertible.ToInt32(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		long IConvertible.ToInt64(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		float IConvertible.ToSingle(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		string IConvertible.ToString(IFormatProvider provider)
		{
			return ToString(provider);
		}

		object IConvertible.ToType(Type conversionType, IFormatProvider provider)
		{
		    if (conversionType == typeof (Date))
		        return this;
			if (conversionType == typeof(DateTime))
				return ToDateTime();
			if (conversionType == typeof (string))
                return ToString(provider);
			if (conversionType == typeof(object))
				return (object)this;
			
			throw new InvalidCastException();
		}

		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		#endregion
	}
}
