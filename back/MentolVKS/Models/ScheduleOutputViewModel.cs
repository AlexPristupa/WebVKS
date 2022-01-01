using MentolVKS.Model.Enums;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MentolVKS.Models
{
    public class ScheduleOutputViewModel
    {
        [EnumDataType(typeof(NameSchedule))]
        [JsonConverter(typeof(StringEnumConverter))]
        public NameSchedule NameSchedule { get; set; }
        public string Schedule { get; set; }
        public DateTime? DateStart { get; set; }
        public int TimeZone { get; set; }
        public string ScheduleText { get; set; }

        public DateTime? NextRun { get; set; }

    }
}
