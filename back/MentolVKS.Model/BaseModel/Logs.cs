using MentolVKS.Model.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.BaseModel
{
	public class Logs : TableBasedEntityBase
	{
		public int Idr { get; set; } // int
		public int? ProductId { get; set; } // int
		public int TypeId { get; set; } // int
		public byte? LevelId { get; set; } // tinyint
		public string UserName { get; set; } // varchar(250)
		public DateTime? DateRecord { get; set; } // datetime
		public string Action { get; set; } // varchar(500)
		public string Description { get; set; } // varchar(2000)
		public string Ip { get; set; } // varchar(200)
		public int? ObjectId { get; set; } // int
		public int? PropertyId { get; set; } // smallint
	}

}
