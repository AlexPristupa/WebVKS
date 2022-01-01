using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.Filters
{
    /// <summary>
    /// Изменить имя фильтра
    /// </summary>
    public class RenameFilter
    {
        /// <summary>
        /// Идентификатор фильтра
        /// </summary>
        public int FilterId { get; set; }
        /// <summary>
        /// Новое имя фильтра
        /// </summary>
        public string NewNameFilter { get; set; }
    }
}
