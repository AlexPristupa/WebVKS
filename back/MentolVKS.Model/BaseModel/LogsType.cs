using MentolVKS.Model.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.BaseModel
{
    public class LogsType : TableBasedEntityBase
    {
        public int Idr { get; set; } // int
        public string Name { get; set; } // varchar(150)
        public string PrivateName { get; set; } // varchar(128)
    }
}
