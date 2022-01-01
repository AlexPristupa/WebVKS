using MentolVKS.Model.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.BaseModel
{
	public class VksCallInstancesStatus : TableBasedEntityBase
	{
		public int Idr { get; set; } // int
		public int CallInstanceID { get; set; } // int
		public string State { get; set; } // varchar(10)
		public int? DurationSeconds { get; set; } // int
		public string Direction { get; set; } // varchar(10)
		public Guid? SipCallID { get; set; } // uniqueidentifier
		public Guid? GroupID { get; set; } // uniqueidentifier
		public string EncryptedMedia { get; set; } // varchar(5)
		public string UnencryptedMedia { get; set; } // varchar(5)
		public string Layout { get; set; } // varchar(20)
		public string CameraControlAvailable { get; set; } // varchar(5)
		public string RxAudioCodec { get; set; } // varchar(10)
		public decimal? RxAudioCodecPacketLossPercentage { get; set; } // decimal(5, 2)
		public decimal? RxAudioJitter { get; set; } // numeric(18, 0)
		public decimal? RxAudioBitRate { get; set; } // numeric(18, 0)
		public decimal? RxAudioGainApplied { get; set; } // decimal(5, 2)
		public string TxAudioCodec { get; set; } // varchar(10)
		public decimal? TxAudioCodecPacketLossPercentage { get; set; } // decimal(5, 2)
		public decimal? TxAudioJitter { get; set; } // numeric(18, 0)
		public decimal? TxAudioBitRate { get; set; } // numeric(18, 0)
		public decimal? TxAudioRoundTripTime { get; set; } // numeric(18, 0)
		public string TxVideoRole { get; set; } // varchar(20)
		public string TxVideoCodec { get; set; } // varchar(20)
		public decimal? TxVideoWidth { get; set; } // numeric(18, 0)
		public decimal? TxVideoHeight { get; set; } // numeric(18, 0)
		public decimal? TxVideoFrameRate { get; set; } // decimal(4, 1)
		public decimal? TxVideoBitRate { get; set; } // numeric(18, 0)
		public decimal? TxVideoPacketLossPercentage { get; set; } // decimal(5, 2)
		public decimal? TxVIdeoJitter { get; set; } // numeric(18, 0)
		public decimal? TxVIdeoRoundTripTime { get; set; } // numeric(18, 0)
		public DateTime DateLastRecord { get; set; } // datetime
	}
}
