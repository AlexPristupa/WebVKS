using MentolVKS.Model.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.BaseModel
{
	public class VksParticipant : TableBasedEntityBase
	{
		public int Idr { get; set; } // int
		public Guid GUID { get; set; } // uniqueidentifier
		public Guid? CallGUID { get; set; } // uniqueidentifier
		public Guid? CallBridgeGUID { get; set; } // uniqueidentifier
		public string UserJid { get; set; } // varchar(100)
		public string NameUser { get; set; } // varchar(100)
		public string URI { get; set; } // varchar(100)
		public string OriginalURI { get; set; } // varchar(100)
		public short? NumCallLegs { get; set; } // smallint
		public bool? IsActivator { get; set; } // bit
		public bool? CanMove { get; set; } // bit
		public string DefaultLayout { get; set; } // varchar(20)
		public string State { get; set; } // varchar(20)
		public bool? CameraControlAvailable { get; set; } // bit
		public DateTime DateLastRecord { get; set; } // datetime
	}
}
