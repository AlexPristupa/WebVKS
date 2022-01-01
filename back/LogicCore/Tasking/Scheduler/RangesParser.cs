using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicCore.Extensions;

namespace LogicCore.Tasking.Scheduler
{
	internal static class RangesParser
	{
		public static IEnumerable<int> FromString(string ranges, char splitter)
		{
			return FromString(ranges.Split(splitter));
		}

		public static IEnumerable<int> FromString(string[] ranges)
		{
			foreach (string s in ranges)
			{
				// try and get the number
				int num;
				if (TryParse(s, out num))
				{
					yield return num;
					continue; // skip the rest
				}

				// otherwise we might have a range
				// split on the range delimiter
				string[] subs = s.Split('-');
				int start, end;

				// now see if we can parse a start and end
				if (subs.Length > 1 &&
					TryParse(subs[0], out start) &&
					TryParse(subs[1], out end) &&
					end >= start)
				{
					// create a range between the two values
					int rangeLength = end - start + 1;
					foreach (int i in Enumerable.Range(start, rangeLength))
					{
						yield return i;
					}
				}
			}
		}

		private static bool TryParse(string s, out int num)
		{
			return int.TryParse(s, out num);
		}

		public static string ToString(int[] list, char splitter)
		{
			var retString = new StringBuilder();
			Array.Sort(list);

			bool inRangeFind = false;
			int firstInRange = list[0];
			int lastNumber = firstInRange;
			bool first = true;

			for (int i = 1; i < list.Length; i++)
			{
				if (list[i] == (lastNumber + 1))
				{
					inRangeFind = true;
				}
				else
				{
					if (inRangeFind)
					{
						if (!first)
						{
							retString.Append(splitter);
						}
						retString.Append(firstInRange);
						retString.Append("-");
					}
					else
					{
						if (!first)
						{
							retString.Append(splitter);
						}
					}

					retString.Append(lastNumber);

					firstInRange = list[i];
					inRangeFind = false;
					first = false;
				}

				lastNumber = list[i];
			}


			if (inRangeFind)
			{
				if (!first)
				{
					retString.Append(splitter);
				}
				retString.Append(firstInRange);
				retString.Append("-");
			}
			else
			{
				if (!first)
				{
					retString.Append(splitter);
				}
			}
			retString.Append(lastNumber);

			return retString.ToString();
		}
	}
}
