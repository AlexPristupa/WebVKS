using MentolVKS.Model.BaseModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.ViewModel
{
    public class FilterListViewModel
    {
        public IEnumerable<SelectListItem> SelectFilterList { get; set; }
        public FiltersList FiltersList { get; set; }
        public string TableName { get; set; }
    }
}
