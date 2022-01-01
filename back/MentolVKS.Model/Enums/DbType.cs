using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MentolVKS.Model.Enums
{
    /// <summary>
    /// Поддерживаемые типы БД
    /// </summary>
    public enum DbType
    {
        /// <summary>
        /// MsSql
        /// </summary>
        [Display(Name = "MsSql")]
        MsSql,
        /// <summary>
        /// PostgreSQL
        /// </summary>
        [Display(Name = "PostgreSQL")]
        PostgreSQL
    }
}
