using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.Filters.Dto
{
    /// <summary>
    /// Фильтр для нескольких строк для таблицы
    /// </summary>
    public class OptionsFilter : FilterBase
    {
        /// <summary>
        /// Значения строк
        /// </summary>
        public IList<string> Selected { get; set; }

        /// <summary>
        /// Инициализация экземпляра класса OptionsFilter
        /// </summary>
        public OptionsFilter()
        {
            Selected = new List<string>();
            Type = FilterType.Options;
        }
    }
}
