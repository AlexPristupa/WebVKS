namespace MentolVKS.Model.Filters.Dto
{
    /// <summary>
    /// Числовой фильтр для таблицы
    /// </summary>
    public class NumberFilter : FilterBase
    {
        /// <summary>
        /// Оператор фильтра
        /// </summary>
        public OperationType Operator { get; set; }

        /// <summary>
        /// Значение фильтра
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Инициализация экземпляра класса NumberFilter
        /// </summary>
        public NumberFilter()
        {
            Type = FilterType.Number;
        }
    }
}