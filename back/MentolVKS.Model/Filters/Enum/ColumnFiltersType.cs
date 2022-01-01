using System.ComponentModel.DataAnnotations;

namespace MentolVKS.Model.Filters.Dto
{
    /// <summary>
    /// Тип фильтра колонки
    /// </summary>
    public enum ColumnFiltersType
    {
        /// <summary>
        /// Выбор
        /// </summary>
        [Display(Name = "Select")]
        Select = 1,

        /// <summary>
        /// Дата
        /// </summary>
        [Display(Name = "Date")]
        Date = 2,

        /// <summary>
        /// Число
        /// </summary>
        [Display(Name = "Integer")]
        Integer = 3,

        /// <summary>
        /// Строка
        /// </summary>
        [Display(Name = "String")]
        String = 4,

        /// <summary>
        /// Время
        /// </summary>
        [Display(Name = "Time")]
        Time = 5,

        /// <summary>
        /// Дерево
        /// </summary>
        [Display(Name = "Tree")]
        Tree = 6,

        /// <summary>
        /// Выбор Full text search
        /// </summary>
        [Display(Name = "SelectFts")]
        SelectFts = 7,

        /// <summary>
        /// Строка Full text search
        /// </summary>
        [Display(Name = "StringFts")]
        StringFts = 8,

        /// <summary>
        /// Структура Full text search
        /// </summary>
        [Display(Name = "StructureFts")]
        StructureFts = 9,

        /// <summary>
        /// Логический тип (да/не указано)
        /// </summary>
        [Display(Name = "Boolean")]
        Boolean = 10,

        /// <summary>
        /// Выбор PK
        /// </summary>
        [Display(Name = "SelectPk")]
        SelectPk = 11,

#warning Не используется?
        /// <summary>
        /// Дата в строке
        /// </summary>
        [Display(Name = "DateAsString")]
        DateAsString
    }
}