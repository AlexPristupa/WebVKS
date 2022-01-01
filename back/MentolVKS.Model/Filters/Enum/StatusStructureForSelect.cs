using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MentolVKS.Model.Filters.Enum
{
    /// <summary>
    /// Статусы выбора структуры для фильтра select
    /// </summary>
    public enum StatusStructureForSelect
    {
        /// <summary>
        /// Фильтр не используется
        /// </summary>
        [Display(Name = "no")]
        NoStructure,

        /// <summary>
        /// Фильтр используется
        /// </summary>
        [Display(Name = "yes")]
        YesStructure,

        /// <summary>
        /// Фильтр переиспользуется
        /// </summary>
        [Display(Name = "reuse")]
        ReuseStructure,
    }
}
