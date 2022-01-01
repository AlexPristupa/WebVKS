using System;
using System.Collections.Generic;
using MentolVKS.Model.Bases;

namespace MentolVKS.Model.BaseModel
{
    public class Booking : TableBasedEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int? OwnerId { get; set; }
        public DateTime? DateStart { get; set; }
        public int? Timezone { get; set; }
        public int? Duration { get; set; }
        public bool? IsUsePin { get; set; }
        public string Schedule { get; set; }
        public int? SpaceId { get; set; }
        public int? ConnectionTypeId { get; set; }
        public int? AttemptsCount { get; set; }
        public int? Delay { get; set; }
        public bool? IsSendNotification { get; set; }
        public bool? IsSyncToExchange { get; set; }
        public int? OpenConferenceBefore { get; set; }
        public bool? IsNeverUsePin { get; set; }
        public DateTime? DateEnd { get; set; }
        public int? RepeatCount { get; set; }
        public int? PinPoliticsId { get; set; }
        public string PinSchedule { get; set; }
        public DateTime? PinDateStart { get; set; }
        public String ScheduleTab { get; set; }
        public String PinScheduleTab { get; set; }
        public int? TypeId { get; set; }
        public string PinCode { get; set; }
        public string Uid { get; set; }
        public List<LinkBookingToParticipant> LinkBookingToParticipants { get; set; } = new List<LinkBookingToParticipant>();

        /// <summary>
        /// Внешние участники.
        /// </summary>
        public List<LinkBookingTovksUsersOther> LinkBookingTovksUsersOthers { get; set; } = new List<LinkBookingTovksUsersOther>();
        public VksUser Owner { get; set; }
    }
}
