using System;

namespace MentolVKS.Model.Filters.Dto
{
    /// <summary>
    /// Фильтр даты для таблицы
    /// </summary>
    public class DateFilter : FilterBase
    {
        /// <summary>
        /// Начало периода
        /// </summary>
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// Конец периода
        /// </summary>
        public DateTime? ToDate { get; set; }

        /// <summary>
        /// Инициализация экземпляра класса DateFilter
        /// </summary>
        public DateFilter()
        {
            Type = FilterType.Date;
        }
    }
}