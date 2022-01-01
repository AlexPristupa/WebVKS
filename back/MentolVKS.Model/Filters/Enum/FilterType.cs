namespace MentolVKS.Model.Filters.Dto
{
    /// <summary>
    /// Тип фильтра для таблицы
    /// </summary>
    public enum FilterType
    {
        /// <summary>
        /// Фильтр даты
        /// </summary>
        Date,

        /// <summary>
        /// Фильтр времени
        /// </summary>
        Time,

        /// <summary>
        /// Числовой фильтр
        /// </summary>
        Number,

        /// <summary>
        /// Текстовый фильтр
        /// </summary>
        String,

        /// <summary>
        /// Фильтр для нескольких строк
        /// </summary>
        Options,

        /// <summary>
        /// Фильтр MOS
        /// </summary>
        Mos,

        /// <summary>
        /// Фильтр списка значений
        /// </summary>
        ListValue,

        /// <summary>
        /// Фильтр налиция значения
        /// </summary>
        Exists
    }
}