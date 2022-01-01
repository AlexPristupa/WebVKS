using MentolVKS.Model.Bases;

namespace MentolVKS.Model.BaseModel
{
    /// <summary>
    /// Модель связи бронирования с участником.
    /// </summary>
    public class LinkBookingToParticipant : TableBasedEntityBase
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор бронирования.
        /// </summary>
        public int BookingId { get; set; }

        /// <summary>
        /// Идентификатор участника.
        /// </summary>
        public int VksParticipantId { get; set; }

        /// <summary>
        /// Ссылка на участника.
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// Идентификатор профиля.
        /// </summary>
        public string CallLegProfileGuid { get; set; }

        /// <summary>
        /// Данные участника.
        /// </summary>
        public VksUser VksParticipant { get; set; }
    }
}
