using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentolVKS.Models
{
    public class RoleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ViewName { get; set; }
        public int? ParentId { get; set; }
    }
}
