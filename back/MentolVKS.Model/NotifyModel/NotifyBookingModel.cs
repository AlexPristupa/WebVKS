using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.NotifyModel
{
    public class NotifyBookingModel
    {
        [JsonProperty("Text_User")]
        public string TextUser { get; set; }
        [JsonProperty("vksUser_name")]
        public string VksUserName { get; set; }
        [JsonProperty("vksUser_email")]
        public string VksUserEmail { get; set; }

        [JsonProperty("booking_name")]
        public string BookingName { get; set; }
        [JsonProperty("booking_datestart")]
        public string BookingDateStart { get; set; }
        [JsonProperty("booking_duration")]
        public string BookingDuration { get; set; }
        [JsonProperty("booking_timezone_name")]
        public string BookingTimeZoneName { get; set; }
        [JsonProperty("booking_pincode")]
        public string BookingPinCode { get; set; }
        [JsonProperty("booking_openconferencebefore")]
        public string BookingOpenConferenceBefore { get; set; }
        [JsonProperty("space_uri")]
        public string SpaceUri { get; set; }
        [JsonProperty("booking_location")]
        public string BookingLocation { get; set; }
        [JsonProperty("space_callid")]
        public string SpaceCallId { get; set; }
        [JsonProperty("space_passwordguest")]
        public string SpacePasswordGuest { get; set; }

        [JsonProperty("vksusers_emails")]
        public List<String> VksUsersEmails { get; set; } = new List<string>();
        [JsonProperty("booking_timezone_id")]
        public string BookingTimeZoneId { get; set; }

        [JsonProperty("operation_name")]
        public string OperationName { get; set; }

        [JsonProperty("uid")]
        public String Uid { get; set; }
    }
}
