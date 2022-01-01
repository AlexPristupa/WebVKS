using MentolVKS.Model.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.BaseModel
{
    public class ColumnForStringFilter : ViewBasedEntityBase
    {
        public ColumnForStringFilter() { }
        public ColumnForStringFilter(int id)
        {
            Id = id;
        }
        public long Id { get; set; }
        public string Value { get; set; }
        public string State { get; set; }  
    }
}
