using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogicCore.Common
{
	/// <summary>
	///	 Класс, объединяющий в себе пару объектов
	///	 Иногда бывает полезно объединить два значения в пару, но частный класс городить не хочется
	///	 Можно использовать этот класс
	/// </summary>
	/// <typeparam name="T1">Тип первого параметра</typeparam>
	/// <typeparam name="T2">Тип второго параметра</typeparam>
	[Serializable]
	public class Pair<T1, T2> : IPair<T1, T2>
	{
		private Pair()
		{
		}

		public Pair(Pair<T1, T2> pair)
		{
			First = pair.First;
			Second = pair.Second;
		}

		public Pair(KeyValuePair<T1, T2> pair)
		{
			First = pair.Key;
			Second = pair.Value;
		}

		public Pair(T1 first, T2 second)
		{
			First = first;
			Second = second;
		}

		/// <summary>
		///	 Первый параметр
		/// </summary>
		public T1 First { get; private set; }

		/// <summary>
		///	 Второй параметр
		/// </summary>
		public T2 Second { get; private set; }

		int IComparable.CompareTo(object obj)
		{
			return CompareTo(obj, Comparer<object>.Default);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj, EqualityComparer<object>.Default);
		}


		private bool Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			var pair = other as Pair<T1, T2>;
			if (pair == null || !comparer.Equals(First, pair.First))
			{
				return false;
			}
			else
			{
				return comparer.Equals(Second, pair.Second);
			}
		}


		private int CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			var pair = other as Pair<T1, T2>;
			if (pair == null)
			{
				throw new ArgumentNullException();
			}
			else
			{
				int num = comparer.Compare(First, pair.First);
				if (num != 0)
				{
					return num;
				}
				else
				{
					return comparer.Compare(Second, pair.Second);
				}
			}
		}


		public override int GetHashCode()
		{
			return GetHashCode(EqualityComparer<object>.Default);
		}

		private int GetHashCode(IEqualityComparer comparer)
		{
			unchecked
			{
				return comparer.GetHashCode(First) * 256 * 256 + comparer.GetHashCode(Second);
			}
		}

		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.Append("(");
			return ToString(sb);
		}

		private string ToString(StringBuilder sb)
		{
			sb.Append(First);
			sb.Append(", ");
			sb.Append(Second);
			sb.Append(")");
			return (sb).ToString();
		}

		public static Dictionary<T1, T2> ToDictionary(IEnumerable<Pair<T1, T2>> list)
		{
			var result = list.ToDictionary(pair => pair.First, pair => pair.Second);
			return result;
		}

	}

	public interface IPair<out T, out U> : IComparable
	{
		T First { get; }
		U Second { get; }
	}

    public static class Pair {

        public static Pair<T1, T2> From<T1, T2>(T1 first, T2 second)
        {
            return new Pair<T1, T2>(first, second);
        }
    }

}