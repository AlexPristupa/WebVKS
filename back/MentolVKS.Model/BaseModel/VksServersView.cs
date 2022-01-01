using MentolVKS.Model.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MentolVKS.Model.BaseModel
{
    public class VksServersView : ViewBasedEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BasicPath { get; set; }
        public int? ServersGroupsId { get; set; }
    }
}
