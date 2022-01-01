using MentolVKS.Model.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.BaseModel
{
    public class FilterColumn : ViewBasedEntityBase
    {
        public int Id { get; set; }
        public string ColumnName { get; set; }
        public int TypeId { get; set; }
        public string TypeName { get; set; }
    }
}
