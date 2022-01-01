using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.Filters.Dto
{
    /// <summary>
    /// Текстовый фильтр таблицы
    /// </summary>
    public class StringFilter : FilterBase
    {
        /// <summary>
        /// Данные фильра
        /// </summary>
        public List<StringFilterValue> Values { get; set; }

        /// <summary>
        /// Инициализация экземпляра класса StringFilter
        /// </summary>
        public StringFilter()
        {
            Type = FilterType.String;
        }
    }

    /// <summary>
    /// Данные фильтра
    /// </summary>
    public class StringFilterValue
    {
        /// <summary>
        /// Значение фильтра
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Оператор фильтра
        /// </summary>
        public StringOperationType Operator { get; set; }
    }
}
