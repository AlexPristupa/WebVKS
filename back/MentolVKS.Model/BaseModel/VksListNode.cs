using MentolVKS.Model.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.BaseModel
{
	public class VksListNode : TableBasedEntityBase
	{
		public int Idr { get; set; } // int
		public int ServersID { get; set; } // int
		public DateTime DateRecord { get; set; } // datetime
		public string Name { get; set; } // varchar(256)
		public Guid CallBridgeId { get; set; } // uniqueidentifier
		public string Description { get; set; } // varchar(256)
		public string Ipv4 { get; set; } // varchar(50)
		public bool IsDeleted { get; set; } // bit
	}
}
