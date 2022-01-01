using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MentolVKS.Model.Filters.Enum
{
    /// <summary>
    /// Результат сохранения колонок
    /// </summary>
    public enum SaveColumnResult
    {
        /// <summary>
        /// Удачное выполнение
        /// </summary>
        [Display(Name = "Success")]
        Success,
        /// <summary>
        /// Последняя колонка в таблице
        /// </summary>
        [Display(Name = "Last Column")]
        LastColumn,
        /// <summary>
        /// Обязательная колонка
        /// </summary>
        [Display(Name = "Required Column")]
        RequiredColumn,
        /// <summary>
        /// Колонка не найдена
        /// </summary>
        [Display(Name = "Not Found")]
        NotFound
    }
}
