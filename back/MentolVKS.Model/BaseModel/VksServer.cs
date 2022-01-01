using MentolVKS.Model.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.BaseModel
{
	public class VksServer : TableBasedEntityBase
	{
		public int Idr { get; set; } // int
		public string Name { get; set; } // varchar(100)
		public string RemoteIPAddress { get; set; } // varchar(64)
		public int Port { get; set; } // int
		public string Login { get; set; } // varchar(100)
		public string Password { get; set; } // varchar(100)
		public string Mode { get; set; } // varchar(6)
		public string APIVersion { get; set; } // varchar(6)
		public char? ResultsSiteName { get; set; } // varchar(1)
		public string Version { get; set; } // varchar(256)
		public string VersionSoftware { get; set; } // varchar(256)
		public int? VendorID { get; set; } // int
		public int? ModelID { get; set; } // int
		public bool? Enable { get; set; } // bit
		public int ServersGroupId { get; set; }
	}
}
