using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MentolVKS.Common.TypeExtensions
{
    /// <summary>
    /// Расширение для перечислений
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Возвращает имя члена перечисления
        /// </summary>
        /// <param name="enumValue">Член перечисления</param>
        /// <returns>Имя члена перечисления</returns>
        public static string GetDisplayName(this Enum enumValue)
        {
            if (enumValue == null) return string.Empty;

            DisplayAttribute attribute = null;

            var member = enumValue.GetType().GetMember(enumValue.ToString()).FirstOrDefault();
            if (member != null) attribute = member.GetCustomAttribute<DisplayAttribute>();

            return attribute != null ? attribute.Name : enumValue.ToString();
        }

        /// <summary>
        /// Возвращает обисание члена перечисления
        /// </summary>
        /// <param name="enumValue">Член перечисления</param>
        /// <returns>Описание члена перечисления</returns>
        public static string GetDescription(this Enum enumValue)
        {
            var fi = enumValue.GetType().GetField(enumValue.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : enumValue.ToString();
        }

        /// <summary>
        /// Возвращает все члены перечисления
        /// </summary>
        /// <typeparam name="TEnum">Тип перечисления</typeparam>
        /// <returns>Список членов перечисления</returns>
        public static IEnumerable<TEnum> GetValues<TEnum>()
        {
            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
        }

        /// <summary>
        /// Возвращает член перечисления по имени
        /// </summary>
        /// <param name="name">Отображаемое имя перечисления</param>
        /// <returns>Член перечисления</returns>
        public static T GetEnumValueFromName<T>(this string name)
        {
            var type = typeof(T);

            if (!type.IsEnum) throw new InvalidOperationException();

            foreach (var field in type.GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(DisplayAttribute)) is DisplayAttribute attribute)
                {
                    if (attribute.Name == name) return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == name) return (T)field.GetValue(null);
                }
            }

            throw new ArgumentOutOfRangeException(nameof(name));
        }
    }
}
