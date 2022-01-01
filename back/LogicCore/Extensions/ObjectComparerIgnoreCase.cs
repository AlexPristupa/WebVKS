using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace LogicCore.Extensions
{
    class ObjectComparerIgnoreCase<T> : IEqualityComparer<T>//, IEquatable<T> //where T : class
    {
        static ConcurrentDictionary<Type, ObjectComparerIgnoreCase<T>> _cache = new ConcurrentDictionary<Type, ObjectComparerIgnoreCase<T>>();

        public static IEqualityComparer<T> GetComparer()
        {
            if (typeof(T) == typeof(string))
                return (IEqualityComparer<T>)StringComparer.OrdinalIgnoreCase;
            var properties = typeof(T).GetProperties();
            if (properties.Any(p => p.PropertyType == typeof(string)))
            {
                return _cache.GetOrAdd(typeof(T), t =>
                    new ObjectComparerIgnoreCase<T>(properties));
            }
            return EqualityComparer<T>.Default;
        }

        private readonly PropertyFuncInfo<T>[] _properties;

        ObjectComparerIgnoreCase(PropertyInfo[] properties)
        {
            var funcs = new List<PropertyFuncInfo<T>>();
            foreach (var propertyInfo in properties)
            {
                var instance = Expression.Parameter(propertyInfo.DeclaringType, "i");
                var property = Expression.Property(instance, propertyInfo);
                var convert = Expression.TypeAs(property, typeof(object));
                var func = (Func<T, object>)Expression.Lambda(convert, instance).Compile();
                funcs.Add(new PropertyFuncInfo<T> { GetValue = func, PropertyType = propertyInfo.PropertyType });
            }
            _properties = funcs.ToArray();
        }

        public bool Equals(T x, T y)
        {
            for (int i = 0; i < _properties.Length; i++)
            {
                var propInfo = _properties[i];
                var xval = propInfo.GetValue(x);
                var yval = propInfo.GetValue(y);
                if (propInfo.PropertyType == typeof(string))
                {
                    if (StringComparer.OrdinalIgnoreCase.Equals(xval, yval) == false)
                        return false;
                }
                else
                {
                    if (object.Equals(xval, yval) == false)
                        return false;
                }
            }
            return true;
        }

        public int GetHashCode(T obj)
        {
            var hash = 0;
            for (int i = 0; i < _properties.Length; i++)
            {
                var val = _properties[i].GetValue(obj);
                if (val != null)
                {
                    if (val is string s)
                        hash *= StringComparer.OrdinalIgnoreCase.GetHashCode(s);
                    else
                        hash *= val.GetHashCode();
                }
            }
            return hash;
        }
    }

    /*
     GroboIL
     https://habr.com/ru/company/skbkontur/blog/262711/
   */

    /*
https://stackoverflow.com/questions/3060382/comparing-2-objects-and-retrieve-a-list-of-fields-with-different-values
public static class PropertyComparer<T>
{
    private static readonly Func<T, T, List<string>> getDeltas;
    static PropertyComparer()
    {
        var dyn = new DynamicMethod(":getDeltas", typeof (List<string>), new[] {typeof (T), typeof (T)},typeof(T));
        var il = dyn.GetILGenerator();
        il.Emit(OpCodes.Newobj, typeof (List<string>).GetConstructor(Type.EmptyTypes));
        bool isValueType = typeof (T).IsValueType;
        OpCode callType = isValueType ? OpCodes.Call : OpCodes.Callvirt;
        var add = typeof(List<string>).GetMethod("Add");
        foreach (var prop in typeof(T).GetProperties())
        {
            if (!prop.CanRead) continue;
            Label next = il.DefineLabel();
            switch (Type.GetTypeCode(prop.PropertyType))
            {
                case TypeCode.Boolean:
                case TypeCode.Byte:
                case TypeCode.Char:
                case TypeCode.Double:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.SByte:
                case TypeCode.Single:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    if(isValueType) {il.Emit(OpCodes.Ldarga_S, (byte)0);} else {il.Emit(OpCodes.Ldarg_0);}
                    il.EmitCall(callType, prop.GetGetMethod(), null);
                    if (isValueType) { il.Emit(OpCodes.Ldarga_S, (byte)1); } else { il.Emit(OpCodes.Ldarg_1); }
                    il.EmitCall(callType, prop.GetGetMethod(), null);
                    il.Emit(OpCodes.Ceq);
                    break;
                default:
                    var pp = new Type[] {prop.PropertyType, prop.PropertyType};
                    var eq = prop.PropertyType.GetMethod("op_Equality", BindingFlags.Public | BindingFlags.Static, null, pp, null);
                    if (eq != null)
                    {
                        if (isValueType) { il.Emit(OpCodes.Ldarga_S, (byte)0); } else { il.Emit(OpCodes.Ldarg_0); }
                        il.EmitCall(callType, prop.GetGetMethod(), null);
                        if (isValueType) { il.Emit(OpCodes.Ldarga_S, (byte)1); } else { il.Emit(OpCodes.Ldarg_1); }
                        il.EmitCall(callType, prop.GetGetMethod(), null);
                        il.EmitCall(OpCodes.Call, eq, null);

                    }
                    else
                    {
                        il.EmitCall(OpCodes.Call, typeof(EqualityComparer<>).MakeGenericType(prop.PropertyType).GetProperty("Default").GetGetMethod(), null);
                        if (isValueType) { il.Emit(OpCodes.Ldarga_S, (byte)0); } else { il.Emit(OpCodes.Ldarg_0); }
                        il.EmitCall(callType, prop.GetGetMethod(), null);
                        if (isValueType) { il.Emit(OpCodes.Ldarga_S, (byte)1); } else { il.Emit(OpCodes.Ldarg_1); }
                        il.EmitCall(callType, prop.GetGetMethod(), null);
                        il.EmitCall(OpCodes.Callvirt, typeof(EqualityComparer<>).MakeGenericType(prop.PropertyType).GetMethod("Equals", pp), null);
                    }
                    break;
            }
            il.Emit(OpCodes.Brtrue_S, next); // equal
            il.Emit(OpCodes.Dup);
            il.Emit(OpCodes.Ldstr, prop.Name);
            il.EmitCall(OpCodes.Callvirt, add, null);
            il.MarkLabel(next);
        }
        il.Emit(OpCodes.Ret);
        getDeltas = (Func<T, T, List<string>>)dyn.CreateDelegate(typeof (Func<T, T, List<string>>));
    }
    public static List<string> GetDeltas(T x, T y) { return getDeltas(x, y); }

}
*/
    class PropertyFuncInfo<T>
    {
        public Func<T, object> GetValue;
        public Type PropertyType;
    }
}
