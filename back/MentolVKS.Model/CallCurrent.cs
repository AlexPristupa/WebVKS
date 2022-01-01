using MentolVKS.Model.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model
{
    /// <summary>
    /// Модель каректных вызовов
    /// </summary>
    public class CallCurrent : TableBasedEntityBase
    {
        /// <summary>
        /// Первичный ключ (conferenceActive)
        /// </summary>
        public int Idr { get; set; }
        /// <summary>
        /// Внешний ключ
        /// </summary>
        public int ConferenceID { get; set; }
        /// <summary>
        /// CallCorrelatorGUID
        /// </summary>
        public Guid  CallCorrelatorGUID { get; set; }
        /// <summary>
        /// CallType
        /// </summary>
        public string CallType { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// memberCount
        /// </summary>
        public short? NumCallLegs { get; set; }
        /// <summary>
        /// MaxCallLegs
        /// </summary>
        public short? MaxCallLegs { get; set; }
        /// <summary>
        /// NumParticipantsLocal
        /// </summary>
        public short NumParticipantsLocal { get; set; }
        /// <summary>
        /// Locked
        /// </summary>
        public bool? Locked { get; set; }
        /// <summary>
        /// Recording
        /// </summary>
        public bool? Recording { get; set; }
        /// <summary>
        /// RecordingStatus
        /// </summary>
        public bool? RecordingStatus { get; set; }
        /// <summary>
        /// Streaming
        /// </summary>
        public bool? Streaming { get; set; }
        /// <summary>
        /// StreamingStatus
        /// </summary>
        public bool? StreamingStatus { get; set; }
        /// <summary>
        /// AllowAllMuteSelf
        /// </summary>
        public bool? AllowAllMuteSelf { get; set; }
        /// <summary>
        /// AllowAllPresentationContribution
        /// </summary>
        public bool? AllowAllPresentationContribution { get; set; }
        /// <summary>
        /// MessagePosition
        /// </summary>
        public string MessagePosition { get; set; }
        /// <summary>
        /// MessageDuration
        /// </summary>
        public string MessageDuration { get; set; }
        /// <summary>
        /// ActiveWhenEmpty
        /// </summary>
        public bool? ActiveWhenEmpty { get; set; }
        /// <summary>
        /// EndpointRecording
        /// </summary>
        public bool? EndpointRecording { get; set; }
        /// <summary>
        /// DurationSeconds
        /// </summary>
        public int? DurationSeconds { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// StartTime
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// EndTime
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// EndTime
        /// </summary>
        public DateTime? DateLastRecord { get; set; }

        /// <summary>
        /// Список корректных конференций
        /// </summary>
        public ConferenceCurrent ConferenceCurrents { get; set; }

       

    }
}
