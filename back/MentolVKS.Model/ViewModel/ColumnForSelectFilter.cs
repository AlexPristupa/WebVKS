using MentolVKS.Model.BaseModel;
using MentolVKS.Model.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.ViewModel
{
    public class ColumnForSelectFilter : ModelBase
    {
        public IEnumerable<ColumnForStringFilter> ColumnForStringFilter { get; set; }
        public string CheckAll { get; set; }

        /// <summary>
        /// Значения формирования объекта фильтра для отправки в фабрику columnFilter
        /// </summary>
        public string ValueMember { get; set; }
    }
}
