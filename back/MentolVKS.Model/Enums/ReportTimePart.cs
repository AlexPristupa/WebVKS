using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MentolVKS.Model.Enums
{
    /// <summary>
    /// Вид времени периодического выполнения рассылки
    /// </summary>
    public enum ReportTimePart
    {
        /// <summary>
        /// Час
        /// </summary>
        [Display(Name = "hour")]
        Hour,

        /// <summary>
        /// Минута
        /// </summary>
        [Display(Name = "minute")]
        Minute
    }
}
