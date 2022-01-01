using MentolVKS.Model.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.BaseModel
{
    public class ColumnForIntegerFilter : ViewBasedEntityBase
    {
        public long Id { get; set; }
        public string Value { get; set; }
        public string Operand { get; set; }
        public int IsEmptyDate { get; set; }
    }
}
