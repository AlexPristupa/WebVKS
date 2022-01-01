using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MentolVKS.Model.Enums
{
    /// <summary>
    /// Уровень записи лога
    /// </summary>
    public enum LogLevels
    {
        /// <summary>
        /// Success
        /// </summary>
        [Display(Name = "Success")]
        Success = 1,

        /// <summary>
        /// Warning
        /// </summary>
        [Display(Name = "Warning")]
        Warn = 2,

        /// <summary>
        /// Error
        /// </summary>
        [Display(Name = "Error")]
        Error = 3
    }
}
