using System;
using System.Reflection;

namespace LogicCore.Extensions
{
    public static class EnumExtensions
	{
		public static System.Enum[] GetValues(Type enumType)
		{
			Type et = enumType;
			//get the public static fields (members of the enum)
			var fi = et.GetFields(BindingFlags.Static | BindingFlags.Public);
			//create a new enum array
			var values = new System.Enum[fi.Length];
			for (int iEnum = 0; iEnum < fi.Length; iEnum++)
			{
				values[iEnum] = (System.Enum)fi[iEnum].GetValue(null);
			}
			return values;
		}
	}
}
