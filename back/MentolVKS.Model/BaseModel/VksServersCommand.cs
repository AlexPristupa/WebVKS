using MentolVKS.Model.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.BaseModel
{
	public class VksServersCommand : TableBasedEntityBase
	{
		public int Idr { get; set; } // int
		public int Serversid { get; set; } // int
		public bool Enable { get; set; } // bit
		public int Idc { get; set; } // int
		public int? Serviceid { get; set; } // int
		public string Namec { get; set; } // varchar(100)
		public string Command { get; set; } // varchar(100)
		public TimeSpan? Starttime { get; set; } // time(7)
		public string Collectionfrequency { get; set; } // varchar(50)
		public string Fileextension { get; set; } // varchar(100)
		public string Folderdata { get; set; } // varchar(100)
		public DateTime Daterecord { get; set; } // datetime
		public int? Lastdurms { get; set; } // int
		public int? Lastcnt { get; set; } // int
		public int? Errorcount { get; set; } // int
		public int? Nodeid { get; set; } // int
	}
}
