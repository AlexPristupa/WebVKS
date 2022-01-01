namespace MentolVKS.Model.Filters.Dto
{
    /// <summary>
    ///     Результат валидации фильтра.
    /// </summary>
    public class FilterQueryValidateResult
    {
        /// <summary>
        ///     Признак валидности фильтра.
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        ///     Сообщение ошибки.
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}