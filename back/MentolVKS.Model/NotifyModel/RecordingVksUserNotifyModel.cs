using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.NotifyModel
{
    public class RecordingVksUserNotifyModel
    {
        [JsonProperty("userName")]
        public string UserName { get; set; }
        [JsonProperty("userEmail")]
        public string UserEmail { get; set; }
        [JsonProperty("bookingName")]
        public string BookingName { get; set; }
        [JsonProperty("recordingId")]
        public int RecordingId { get; set; }
        [JsonProperty("recordingUrl")]
        public string RecordingUrl { get; set; }
        [JsonProperty("recordingVksUsersIsPlay")]
        public bool RecordingVksUsersIsPlay { get; set; }
        [JsonProperty("recordingVksUsersIsDownload")]
        public bool RecordingVksUsersIsDownload { get; set; }
        [JsonProperty("recordingVksUsersDescription")]
        public string RecordingVksUsersDescription { get; set; }
    }
}
