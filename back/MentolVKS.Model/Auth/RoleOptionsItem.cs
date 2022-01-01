using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentolVKS.Model.Auth
{
    public class RoleOptionsItem
    {
        public int Id { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public int? ParentId { get; set; }
        public string ViewName { get; set; }
    }
}
