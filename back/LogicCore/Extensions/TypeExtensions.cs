using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LogicCore.Common;
using LogicCore;
using System.Runtime.CompilerServices;

namespace LogicCore.Extensions
{
	/// <summary>
	/// 
	/// </summary>
	public static class TypeExtensions
	{

		private static Assembly _msCoreLib;

		static TypeExtensions()
		{
			_msCoreLib = typeof (int).Assembly;
		}

		/// <summary>
		/// Получить тип аргумента шаблона(обобщения) наследующего указанный тип
		/// </summary>
		/// <param name="sourceType"></param>
		/// <param name="argumentBaseType"></param>
		/// <returns></returns>
		public static Type GetTypeGenericArgument(this Type sourceType, Type argumentBaseType)
		{
			var result = sourceType.GetGenericArguments()
								   .SingleOrDefault(argumentBaseType.IsAssignableFrom);
			if (result == null && sourceType.IsInterface)
			{
				var reqType = sourceType.GetInterfaces()
					.Where(i => i.IsGenericType)
					.Select(i =>i.GetGenericArguments().SingleOrDefault(argumentBaseType.IsAssignableFrom));
				result = reqType.FirstOrDefault();
			}
			return result;
		}


        public static Type GetEnumerableElementType(this Type enumerateType)
        {
            var result = enumerateType.GetElementType();
            if (result == null &&
                enumerateType.IsGenericType)
            {
                var subType = enumerateType
                           .GetInterfaces()
                           .Where(t => t.IsGenericType == true
                               && t.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                           .FirstOrDefault();
                if (subType != null)
                {
                    result = subType.GetGenericArguments()[0];
                }
            }
            return result;
        }

        /// <summary>
        /// Проверить является ли тип cистемным(базовым) передающимся по значению или строкой
        /// </summary>
        public static bool IsValueOrStringType(this Type type)
		{
			var result = type.IsValueType || typeof (string).IsAssignableFrom(type);
			return result;
		}

		/// <summary>
		/// Проверить является ли тип cистемным
		/// </summary>
		public static bool IsSystem(this Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			var result = Equals(type.Assembly, _msCoreLib) ||
						 type.Module.Name.StartsWith("System.");
			return result;
		}

		/// <summary>
		/// Тип передается и присваивается по одному значению (в т.ч. системные структуры)
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static bool IsSingleValueType(this Type type)
		{
			var code = Type.GetTypeCode(type);
			var result = code != TypeCode.Object;
			if (result == false)
			{
				if (type == typeof (Guid) ||
					type == typeof (TimeSpan) ||
					type == typeof (Date) ||
					type == typeof(byte[]) ||
					type == typeof (DateTimeOffset) ||
					(type.IsGenericType &&
					 type.GetGenericTypeDefinition() == typeof (Nullable<>)))
				{
					result = true;
				}
			}
			return result;
		}


		/// <summary>
		/// Структура, содержащая одно поле внутри
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static bool IsSingleFieldValueType(this Type type)
		{
			var result = type.IsValueType;
			if (result == false)
			{
				var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				result = fields.Length == 1;
			}
			return result;
		}

		/// <summary>
		/// Проверить, что тип является перечислителем списка (исключаются строки и массивы)
		/// </summary>
		public static bool IsEnumerable(this Type type)
		{
			var result = typeof (IEnumerable).IsAssignableFrom(type) &&
						 type != typeof (string) && type != typeof(byte[]);
			return result;
		}

		/// <summary>
		/// Проверить, что тип является Nullable (type?)
		/// </summary>
		public static bool IsNullable(this Type type)
		{
			var result = type.IsValueType &&
						type.IsGenericType &&
						typeof(Nullable<>) == type.GetGenericTypeDefinition();
			return result;
		}
		
		public static bool IsNumeric(this Type type, bool checkNullable)
		{
			if (type == null || type.IsEnum)
			{
				return false;
			}
			var code = Type.GetTypeCode(type);
			//учитываем Nullable типы
			if (checkNullable &&
				code == TypeCode.Object &&
				type.IsGenericType &&
				type.GetGenericTypeDefinition() == typeof (Nullable<>))
			{
				code = Type.GetTypeCode(Nullable.GetUnderlyingType(type));
			}
			switch (code)
			{
				case TypeCode.SByte:
				case TypeCode.Byte:
				case TypeCode.Int16:
				case TypeCode.UInt16:
				case TypeCode.Int32:
				case TypeCode.UInt32:
				case TypeCode.Int64:
				case TypeCode.UInt64:
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
					return true;
			}
			return false;
		}

		public static IEnumerable<Type> GetBaseTypes(this Type type, bool includeCurrentType)
		{
			var types = new List<Type>();
			if (includeCurrentType)
				types.Add(type);

			while (type.BaseType != typeof (object))
			{
				type = type.BaseType;
				types.Add(type);
			}
			return types;
		}

        /// <summary>
        /// Является ли тип анонимным
        /// </summary>
        public static bool IsAnonymous(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            // HACK: The only way to detect anonymous types right now.
            var result =
                type.IsGenericType
                && (type.Attributes & TypeAttributes.NotPublic) == TypeAttributes.NotPublic
                && (type.Name.StartsWith("<>f__") ||
                         type.Name.StartsWith("VB$AnonymousType"))
                && Attribute.IsDefined(type, typeof(CompilerGeneratedAttribute), false);
            return result;
        }

        public static bool IsDelegate(this Type type)
		{
			var result = typeof (Delegate).IsAssignableFrom(type);
			return result;
		}

		public static void CheckType<TBaseType>(this Type type)
		{
			if (typeof (TBaseType).IsAssignableFrom(type) == false)
			{
				throw new CheckException(
					string.Format("Переданный тип {0} не является производным от {1}",
					typeof(TBaseType), type));
			}
		}

		/// <summary>
		/// Получить список полей и свойств
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static MemberInfo[] GetFieldsOrProperties(this Type type)
		{
		    return GetFieldsOrProperties(type, BindingFlags.Instance | BindingFlags.Public);
		}

        /// <summary>
        /// Получить список полей и свойств
        /// </summary>
        /// <returns></returns>
        public static MemberInfo[] GetFieldsOrProperties(this Type type, BindingFlags flags)
        {
			var fields = type.GetFields(flags);
			var properties = type.GetProperties(flags);
			var result = new MemberInfo[fields.Length + properties.Length];
			if (fields.Length > 0)
				Array.Copy(fields, result, fields.Length);
			if (properties.Length > 0)
				Array.Copy(properties, 0, result, fields.Length, properties.Length);
			return result;
		}

		/// <summary>
		/// Получить список полей и свойств
		/// </summary>
		/// <returns></returns>
		public static MemberInfo GetFieldOrProperty(this Type type, string name)
		{
			MemberInfo result = type.GetProperty(name, BindingFlags.Instance | BindingFlags.Public);
			if (result != null) return result;
			result = type.GetField(name, BindingFlags.Instance | BindingFlags.Public);
			return result;
		}

		/// <summary>
		/// Значение по умолчанию для типа
		/// </summary>
		public static object GetDefaultValue(this Type type)
		{
			object result = null;
			if (type.IsValueType)
			{
				result = Activator.CreateInstance(type);// FormatterServices.GetUninitializedObject(type); //Activator.CreateInstance(type);
			}
			return result;
		}

		public static void CopyProperties(this Type type, object source, object destination, BindingFlags flags = BindingFlags.Instance | BindingFlags.Public)
		{
			var properties = type.GetProperties(flags);
			foreach(var prop in properties)
			{
				var value = prop.GetValue(source);
				prop.SetValue(destination, value);
			}
		}
	}
}
