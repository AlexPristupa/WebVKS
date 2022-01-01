namespace MentolVKS.Model.Filters.Dto
{
    /// <summary>
    /// Типы строковых операндов
    /// </summary>
    public enum StringOperationType
    {
        /// <summary>
        /// Равно
        /// </summary>
        Eq,

        /// <summary>
        /// Не равно
        /// </summary>
        NotEq,

        /// <summary>
        /// Содержит
        /// </summary>
        Like,

        /// <summary>
        /// Не содержит
        /// </summary>
        NotLike,

        /// <summary>
        /// Содержит точно
        /// </summary>
        LikeExactly,

        /// <summary>
        /// Не содержит точно
        /// </summary>
        NotLikeExactly,

        /// <summary>
        /// Содержит слева
        /// </summary>
        LikeLeft,

        /// <summary>
        /// Не содержит слева
        /// </summary>
        NotLikeLeft,

        /// <summary>
        /// Содержит справа
        /// </summary>
        LikeRight,

        /// <summary>
        /// Не содержит справа
        /// </summary>
        NotLikeRight
    }
}
