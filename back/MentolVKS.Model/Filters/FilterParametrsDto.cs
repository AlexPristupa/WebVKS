using System.Collections.Generic;

namespace MentolVKS.Model.Filters.Dto
{
    /// <summary>
    /// Класс фильтра JQuery таблицы
    /// </summary>
    public class FilterParametrsDto
    {
        /// <summary>
        /// Идентификатор фильтра
        /// </summary>
        public int FilterId { get; set; }

        /// <summary>
        /// Фильтр применён
        /// </summary>
        public bool IsApply { get; set; }

        /// <summary>
        /// Имя таблицы
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        ///     Ограничение на элементов в выборке
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// Фильтры колонок
        /// </summary>
        public List<ColumnFiltersDto> ColumnFilters { get; set; } = new List<ColumnFiltersDto>();

        /// <summary>
        /// Имя колонки, по которой произведена сортировка
        /// </summary>
        public string OrderColumnName { get; set; }

        /// <summary>
        /// Направление сортировки
        /// </summary>
        public SortDirection OrderDir { get; set; }

        /// <summary>
        /// Смещение
        /// </summary>
        public long Start { get; set; }

        /// <summary>
        /// Количество записей в выборке
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Строка поиска
        /// </summary>
        public string Search { get; set; }
    }
}