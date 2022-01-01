using MentolVKS.Model.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.BaseModel
{
    public class FilterName : ViewBasedEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IsActive { get; set; }
        public int? IsCommon { get; set; }
    }
}
