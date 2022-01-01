using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.Filters
{
    /// <summary>
    /// Тип фильтр для получения НД из БД
    /// </summary>
    public class TypeSelectFilter
    {
        /// <summary>
        /// Идентификатор фильтра в табд. FilterList 
        /// </summary>
        public int FilterId { get; set; } = -1;
        /// <summary>
        /// Имя колонки фильтрации
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// Имя таблицы из TableColumnSeting
        /// </summary>
        public string TableName { get; set; }
    }
}
