using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MentolVKS.Model.Enums
{
    /// <summary>
    /// Тип расписания
    /// </summary>
    public enum ScheduleMode
    {
        /// <summary>
        /// День
        /// </summary>
        [Display(Name = "day")]
        Day,

        /// <summary>
        /// Неделя
        /// </summary>
        [Display(Name = "week")]
        Week,

        /// <summary>
        /// Месяц
        /// </summary>
        [Display(Name = "month")]
        Month
    }
}
