using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.NotifyModel
{
    public class RecordingNotifyModel
    {
        [JsonProperty("recordingUrl")]
        public string RecordingUrl { get; set; }
        [JsonProperty("bookingName")]
        public string BookingName { get; set; }
        [JsonProperty("vksUsersEmail")]
        public string VksUserEmail { get; set; }
    }
}
