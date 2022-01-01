using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MentolVKS.Model.Filters.Enum
{
    /// <summary>
    /// Результат сохранения колонок
    /// </summary>
    public enum ValueMemberType
    {
        // <summary>
        /// Идентификатор
        /// </summary>
        [Display(Name = "id")]
        Id,
        /// <summary>
        /// Значение
        /// </summary>
        [Display(Name = "value")]
        Value
    }
}
