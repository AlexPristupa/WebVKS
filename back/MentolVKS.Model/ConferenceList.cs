using MentolVKS.Model.Bases;
using System;

namespace MentolVKS.Model
{
    /// <summary>
    /// Модель активных конференций
    /// </summary>
    public class ConferenceList : ViewBasedEntityBase
    {
        /// <summary>
        /// ConferenceId
        /// </summary>
        public int? ConferenceId { get; set; }
        /// <summary>
        /// ConferenceGUID
        /// </summary>
        public Guid ConferenceGUID { get; set; }
        /// <summary>
        /// ConferenceName
        /// </summary>
        public string ConferenceName { get; set; }
        /// <summary>
        /// ConferenceActive
        /// </summary>
        public int? ConferenceActive { get; set; }
        /// <summary>
        /// ConferenceOwner
        /// </summary>
        public string ConferenceOwner { get; set; }
        /// <summary>
        /// ConferenceNumber
        /// </summary>
        public string ConferenceNumber { get; set; }
        /// <summary>
        /// DurationSecond
        /// </summary>
        public int? DurationSecond { get; set; }
        /// <summary>
        /// MemberCount
        /// </summary>
        public short? MemberCount { get; set; }
    }
}
