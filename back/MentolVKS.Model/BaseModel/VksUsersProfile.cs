using MentolVKS.Model.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.BaseModel
{
	public class VksUsersProfile : TableBasedEntityBase
	{
		public int Idr { get; set; } // int
		public Guid GUID { get; set; } // uniqueidentifier
		public bool CanCreateCoSpaces { get; set; } // bit
		public bool CanCreateCalls { get; set; } // bit
		public bool CanUseExternalDevices { get; set; } // bit
		public bool CanMakePhoneCalls { get; set; } // bit
		public bool CanReceiveCalls { get; set; } // bit
		public bool UserToUserMessagingAllowed { get; set; } // bit
		public bool HasLicense { get; set; } // bit
		public DateTime DateLastRecord { get; set; } // datetime
	}
}
