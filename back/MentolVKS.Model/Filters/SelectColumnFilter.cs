using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.Filters
{
    public class SelectColumnFilter
    {
        public string ColumnName { get; set; }
        public string TableName { get; set; }
        public List<string> CheckList { get; set; }
    }
}
