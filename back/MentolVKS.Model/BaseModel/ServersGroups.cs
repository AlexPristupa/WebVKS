using MentolVKS.Model.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.BaseModel
{
    /// <summary>
    /// Группа серверов
    /// </summary>
    public class ServersGroups : TableBasedEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }        
        public string Description { get; set; }
        public int? IsUseBalancer { get; set; }
        public int? BalancerAlgId { get; set; }
    }
}
