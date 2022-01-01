using System.Collections.Generic;

namespace MentolVKS.Model.Filters.Dto
{
    /// <summary>
    /// Класс расширения панели фильтрации
    /// </summary>
    public class ExtensionFilters
    {
        /// <summary>
        /// Имя таблицы
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Тмя поля
        /// </summary>
        public string NameField { get; set; }

        /// <summary>
        /// Тип фильтра
        /// </summary>
        public string FilterType { get; set; }

        /// <summary>
        /// Перечень значений фильтра 
        /// </summary>
        public IEnumerable<object> ValuesField { get; set; }
    }
}
