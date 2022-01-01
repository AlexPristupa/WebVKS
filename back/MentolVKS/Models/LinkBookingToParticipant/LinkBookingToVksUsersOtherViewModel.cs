namespace MentolVKS.Models
{
    /// <summary>
    /// Модель связи бронирования с внешним участником.
    /// </summary>
    public class LinkBookingToVksUsersOtherViewModel
    {
        /// <summary>
        /// Идентификатор участника.
        /// </summary>
        public int? VksUsersOtherId { get; set; }

        /// <summary>
        /// Имя участника.
        /// </summary>
        public string VksUserOtherName { get; set; }

        /// <summary>
        /// Ссылка на участника.
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// Электронный адрес участника.
        /// </summary>
        public string Email { get; set; }
    }
}
