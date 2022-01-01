using System;
using System.Reflection;
using System.Linq.Expressions;


namespace LogicCore.Extensions
{
    public static class MemberInfoExtensions
	{
		/// <summary>
		/// Получить информацию о члене класса
		/// </summary>
		public static MemberInfo GetMember(Expression expression)
			//where TParam : class
		{
			//var body = memberInfo.Body;
			//while (body.NodeType != ExpressionType.MemberAccess)
			//{
			//	if (body.NodeType != ExpressionType.Convert)
			//	{
			//		throw new ArgumentException("Передано неверное выражение, необходимо передать obj => obj.Member", "memberInfo");
			//	}
			//	var convert = (UnaryExpression)body;
			//	body = convert.Operand;
			//}
			//var member = (MemberExpression)body;
			//return member.Member;
			MemberInfo member;
			switch (expression.NodeType)
			{
				case ExpressionType.Lambda:
					var lambdaExpression = (LambdaExpression) expression;
					member = GetMember(lambdaExpression.Body);
					break;
				case ExpressionType.MemberAccess:
					var memberExpression = (MemberExpression) expression;
					//var supername = GetMember(memberExpression.Expression);
					//if (String.IsNullOrEmpty(supername))
					member = memberExpression.Member;
					//return String.Concat(supername, '.', memberExpression.Member.Name);
					break;
				case ExpressionType.Call:
					var callExpression = (MethodCallExpression) expression;
					member = callExpression.Method;
					break;
				case ExpressionType.Convert:
					var unaryExpression = (UnaryExpression) expression;
					member = GetMember(unaryExpression.Operand);
					break;
				case ExpressionType.Parameter:
					member = null;
					break;
				default:
					throw new ArgumentException("The expression is not a member access or method call expression");
			}
			return member;
		}

		/// <summary>
		/// Получить значение, если это свойство или поле
		/// </summary>
		public static object GetFieldOrPropertyValue(this MemberInfo member, object obj)
		{
			object value;
			switch (member.MemberType)
			{
				case MemberTypes.Field:
					value = ((FieldInfo) member).GetValue(obj);
					break;
				case MemberTypes.Property:
					value = ((PropertyInfo) member).GetValue(obj, null);
					break;
				default:
					throw new ArgumentOutOfRangeException("member", "Метод применим только для полей и свойств");
			}
			return value;
		}

		/// <summary>
		/// Присвоить значение, если это свойство или поле
		/// </summary>
		public static void SetFieldOrPropertyValue(this MemberInfo member, object obj, object value)
		{
			switch (member.MemberType)
			{
				case MemberTypes.Field:
					((FieldInfo)member).SetValue(obj, value);
					break;
				case MemberTypes.Property:
					((PropertyInfo)member).SetValue(obj, value, null);
					break;
				default:
					throw new ArgumentOutOfRangeException("member", "Метод применим только для полей и свойств");
			}
		}

		/// <summary>
		/// Получить значение, если это свойство или поле
		/// </summary>
		public static Type GetFieldOrPropertyType(this MemberInfo member)
		{
			Type type;
			switch (member.MemberType)
			{
				case MemberTypes.Field:
					type = ((FieldInfo)member).FieldType;
					break;
				case MemberTypes.Property:
					type = ((PropertyInfo)member).PropertyType;
					break;
				default:
					throw new ArgumentOutOfRangeException("member", "Метод применим только для полей и свойств");
			}
			return type;
		}
	}
}