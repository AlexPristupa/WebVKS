using MentolVKS.Model.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.BaseModel
{
    public class AspNetTreePage : TableBasedEntityBase
    {
        public int Id { get; set; }
        public string ViewName { get; set; }
        public int? ParentId { get; set; }
        public int? RoleId { get; set; }
        public AspNetRole Role { get; set; }
    }
}
