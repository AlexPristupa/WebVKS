using System;

namespace MentolVKS.Model.Filters.Dto
{
    /// <summary>
    /// Фильтр времени таблицы
    /// </summary>
    public class TimeFilter : FilterBase
    {
        /// <summary>
        /// Начало периода
        /// </summary>
        public DateTime? FromTime { get; set; }

        /// <summary>
        /// Конец периода
        /// </summary>
        public DateTime? ToTime { get; set; }

        /// <summary>
        /// Инициализация экземпляра класса TimeFilter
        /// </summary>
        public TimeFilter()
        {
            Type = FilterType.Time;
        }
    }
}