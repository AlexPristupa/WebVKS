using MentolVKS.Model.Bases;
using System;

namespace MentolVKS.Model.BaseModel
{
    public class Recording : TableBasedEntityBase
	{
		public int Id { get; set; }
		public string Url { get; set; }
		public int BookingId { get; set; }
		public DateTime DateStart { get; set; }
		public DateTime DateEnd { get; set; }
		public Booking Booking { get; set; }
	}
}
