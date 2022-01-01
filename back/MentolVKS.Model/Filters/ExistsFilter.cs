using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.Filters.Dto
{
    /// <summary>
    /// Фильтр наличия значения для таблицы
    /// </summary>
    public class ExistsFilter : FilterBase
    {
        /// <summary>
        /// Имеется значение
        /// </summary>
        public bool Exists { get; set; }

        /// <summary>
        /// Инициализация экземпляра класса ExistsFilter
        /// </summary>
        public ExistsFilter()
        {
            Exists = true;
            Type = FilterType.Exists;
        }
    }
}
