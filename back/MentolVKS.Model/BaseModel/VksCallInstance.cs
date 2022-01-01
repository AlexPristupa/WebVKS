using MentolVKS.Model.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.BaseModel
{
	public class VksCallInstance : TableBasedEntityBase
	{
		public int Idr { get; set; } // int
		public Guid GUID { get; set; } // uniqueidentifier
		public Guid CallGUID { get; set; } // uniqueidentifier
		public string RemoteParty { get; set; } // varchar(100)
		public string OriginalRemoteParty { get; set; } // varchar(100)
		public string Name { get; set; } // varchar(100)
		public string LocalAddress { get; set; } // varchar(100)
		public string Type { get; set; } // varchar(10)
		public string Direction { get; set; } // varchar(10)
		public bool? CanMove { get; set; } // bit
		public DateTime? CreateTime { get; set; } // datetime
		public DateTime? StartTime { get; set; } // datetime
		public DateTime? EndTime { get; set; } // datetime
		public DateTime DateLastRecord { get; set; } // datetime
	}
}
