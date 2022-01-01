using MentolVKS.Model.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.BaseModel
{
	public class VksCallInstancesConfig : TableBasedEntityBase
	{
		public int Idr { get; set; } // int
		public int CallInstanceID { get; set; } // int
		public string Name { get; set; } // varchar(100)
		public string DefaultLayout { get; set; } // varchar(20)
		public bool ParticipantLabels { get; set; } // bit
		public bool PresentationContributionAllowed { get; set; } // bit
		public bool PresentationViewingAllowed { get; set; } // bit
		public bool MuteOthersAllowed { get; set; } // bit
		public bool VideoMuteOthersAllowed { get; set; } // bit
		public bool MuteSelfAllowed { get; set; } // bit
		public bool VideoMuteSelfAllowed { get; set; } // bit
		public bool DisconnectOthersAllowed { get; set; } // bit
		public bool TelepresenceCallsAllowed { get; set; } // bit
		public bool SipPresentationChannelEnabled { get; set; } // bit
		public bool ChangeLayoutAllowed { get; set; } // bit
		public string BfcpMode { get; set; } // varchar(20)
		public bool AllowAllMuteSelfAllowed { get; set; } // bit
		public bool AllowAllPresentationContributionAllowed { get; set; } // bit
		public bool ChangeJoinAudioMuteOverrideAllowed { get; set; } // bit
		public bool RecordingControlAllowed { get; set; } // bit
		public bool StreamingControlAllowed { get; set; } // bit
		public DateTime DateLastRecord { get; set; } // datetime
	}
}
