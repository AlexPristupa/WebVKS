using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentolVKS.Models
{
    public class SheduleCheckInputModel
    {
        public string  Schedule { get; set; }
        public DateTime? DateStart { get; set; }
        public int TimeZone { get; set; }
        public int Duration { get; set; }
        public int SpaceId { get; set; }
        public DateTime? DateEnd { get; set; }
        public int RepeatCount { get; set; }
        public int? BookingId { get; set; }
    }
}
