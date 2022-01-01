using MentolVKS.Model.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.BaseModel
{
    public class RefreshLog : TableBasedEntityBase
	{
		public int Idr { get; set; } // int
		public string Info { get; set; } // varchar(2048)
		public DateTime? UploadDate { get; set; } // datetime
		public int ServicesId { get; set; } // int
		public string SitesIds { get; set; } // varchar(1000)
		public int Mode { get; set; } // int
	}
}
