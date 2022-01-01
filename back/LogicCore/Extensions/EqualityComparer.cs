using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LogicCore.Extensions
{
	public sealed class EqualityEquitableComparer<T> : IEqualityComparer<T>
		where T : struct, IEquatable<T>
	{
		public static readonly EqualityEquitableComparer<T> Default = new EqualityEquitableComparer<T>();

		public bool Equals(T x, T y)
		{
			return x.Equals(y);
		}

		public int GetHashCode(T obj)
		{
			return obj.GetHashCode();
		}
	}

	public static class EqualityComparer<T>
	{
		public static readonly IEqualityComparer<T> Default;

		static EqualityComparer()
		{
			if (typeof(T).IsValueType &&
				typeof(IEquatable<T>).IsAssignableFrom(typeof(T)))
			{
				var t = typeof(EqualityEquitableComparer<>).MakeGenericType(typeof(T));
				var field = t.GetField("Default", BindingFlags.Static | BindingFlags.Public);
				var result = field.GetValue(null);
				Default = (IEqualityComparer<T>)result;
				return;
			}
			Default = System.Collections.Generic.EqualityComparer<T>.Default;
		}

	}

}
