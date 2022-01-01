using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.Filters
{
    /// <summary>
    /// DTO запрос колоночного фильтра
    /// </summary>
    public class QueryFilter
    {
        /// <summary>
        /// Тип фильтра
        /// </summary>
        public int FilterId { get; set; }

        /// <summary>
        /// Имя колонки для фильтрации
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// Имя таблицы
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Строка поиска
        /// </summary>
        public string Search { get; set; }

        /// <summary>
        ///     Ограничение на элементов в выборке, дефолтно - 300.
        /// </summary>
        public int? Limit { get; set; } = 300;

        /// <summary>
        /// Дополнительные параметры
        /// </summary>
        public IEnumerable<string> AddParameters { get; set; }

        /// <summary>
        /// Список выбранных значений в колоночном фильтре
        /// </summary>
        public IEnumerable<object> CheckList { get; set; }

        /// <summary>
        /// Статусы переданых ключей подразделений для выбора соответствующих должностей
        /// </summary>
        public string StatusStructureForSelect { get; set; }

    }
}
