using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MentolVKS.Model.Enums
{
    /// <summary>
    /// Тип интервала
    /// </summary>
    public enum IntervalType
    {
        /// <summary>
        /// По установленному времени
        /// </summary>
        [Display(Name = "OnTime")]
        OnTime,

        /// <summary>
        /// Количество в час/минуту
        /// </summary>
        [Display(Name = "Period")]
        Period
    }
}
