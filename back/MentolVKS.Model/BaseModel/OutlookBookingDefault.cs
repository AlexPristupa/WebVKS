using MentolVKS.Model.Bases;
using System;

namespace MentolVKS.Model.BaseModel
{
	public class OutlookBookingDefault : TableBasedEntityBase
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Location { get; set; }
		public string OwnerId { get; set; }
		public string DateStart { get; set; }
		public string Timezone { get; set; }
		public int Duration { get; set; }
		public bool IsUsePin { get; set; }
		public string Schedule { get; set; }
		public string SpaceId { get; set; }
		public string ConnectionTypeId { get; set; }
		public int AtTemptsCount { get; set; }
		public int Delay { get; set; }
		public bool IsSendNotification { get; set; }
		public bool IsSyncToExchange { get; set; }
		public int OpenConferenceBefore { get; set; }
		public bool IsNeverUsePin { get; set; }
		public DateTime? DateEnd { get; set; }
		public int RepeatCount {get;set;}
		public int PinPoliticsId { get; set; }
		public string PinSchedule { get; set; }
		public DateTime? PinDateStart { get; set; }
		public string PinCode { get; set; }
		public int TypeId { get; set; }
		public string ScheduleTab { get; set; }
		public string PinScheduleTab { get; set; }
		public DateTime? LastStart { get; set; }
		public DateTime? PinLastStart { get; set; }
		public string RtfFileTemplate { get; set; }
	}
}
