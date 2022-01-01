using MentolVKS.Model.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.BaseModel
{
	public class NtfEvents : TableBasedEntityBase
	{
		public int Idr { get; set; } //
		public DateTime UploadDate { get; set; } //datetime NOT NULL,
		public int? ServiceId { get; set; } //int NULL,
		public DateTime? ProcessingDate { get; set; } //datetime NULL,
		public int? SubscriptionId { get; set; } //int NULL,
		public string WebPageName { get; set; } //varchar(256) COLLATE Cyrillic_General_CI_AS NULL,
		public string OperationInfo { get; set; } //varchar(128) COLLATE Cyrillic_General_CI_AS NULL,
		public string Param1 { get; set; } //varchar(256) COLLATE Cyrillic_General_CI_AS NULL,
		public string Param2 { get; set; } //varchar(8000) COLLATE Cyrillic_General_CI_AS NULL,
		public string ProcedureName { get; set; } //varchar(50) COLLATE Cyrillic_General_CI_AS NULL,
		public DateTime? Param3 { get; set; } //datetime NULL,
	}

}
