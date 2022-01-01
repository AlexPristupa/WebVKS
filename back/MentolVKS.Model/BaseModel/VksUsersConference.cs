using MentolVKS.Model.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.BaseModel
{
    public class VksUsersConference : TableBasedEntityBase
    {
        public int Idr { get; set; } // int
        public int UserID { get; set; } // int
        public int? ConferenceID { get; set; } // int
    }
}
