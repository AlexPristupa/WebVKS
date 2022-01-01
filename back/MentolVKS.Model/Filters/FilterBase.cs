using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.Filters.Dto
{
    /// <summary>
    /// Базовый класс фильтра таблицы
    /// </summary>
    public abstract class FilterBase
    {
        /// <summary>
        /// Имя столбца
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Тип фильтра
        /// </summary>
        public FilterType Type { get; set; }
    }
}
