using MentolVKS.Model.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.BaseModel
{
    public class VksCallCurrent : TableBasedEntityBase
	{
		public int Idr { get; set; } // int
		public Guid GUID { get; set; } // uniqueidentifier
		public int? ConferenceID { get; set; } // int
		public Guid? CallCorrelatorGUID { get; set; } // uniqueidentifier
		public string CallType { get; set; } // varchar(50)
		public string Name { get; set; } // varchar(100)
		public short? NumCallLegs { get; set; } // smallint
		public short? MaxCallLegs { get; set; } // smallint
		public short? NumParticipantsLocal { get; set; } // smallint
		public bool Locked { get; set; } // bit
		public bool Recording { get; set; } // bit
		public bool RecordingStatus { get; set; } // bit
		public bool Streaming { get; set; } // bit
		public bool StreamingStatus { get; set; } // bit
		public bool AllowAllMuteSelf { get; set; } // bit
		public bool AllowAllPresentationContribution { get; set; } // bit
		public string MessagePosition { get; set; } // varchar(50)
		public string MessageDuration { get; set; } // varchar(50)
		public bool ActiveWhenEmpty { get; set; } // bit
		public bool EndpointRecording { get; set; } // bit
		public int DurationSeconds { get; set; } // int
		public DateTime? CreateTime { get; set; } // datetime
		public DateTime? StartTime { get; set; } // datetime
		public DateTime? EndTime { get; set; } // datetime
		public DateTime DateLastRecord { get; set; } // datetime
	}
}
