namespace MentolVKS.Models
{
    /// <summary>
    /// Модель связи бронирования с участником.
    /// </summary>
    public class LinkBookingToParticipantPostViewModel
    {
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
        /// Эелктронный адрес участника.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Имя участника.
        /// </summary>
        public string VksUserName { get; set; }
    }
}
