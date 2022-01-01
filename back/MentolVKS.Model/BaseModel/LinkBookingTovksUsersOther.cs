using MentolVKS.Model.Bases;

namespace MentolVKS.Model.BaseModel
{
    /// <summary>
    /// Справочник внешних участников.
    /// </summary>
    public class LinkBookingTovksUsersOther : TableBasedEntityBase
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор бронирования.
        /// </summary>
        public int? BookingId { get; set; }

        /// <summary>
        /// Идентификатор участника.
        /// </summary>
        public int? VksUsersOtherId { get; set; }

        /// <summary>
        /// Внешний участник.
        /// </summary>
        public VksUsersOther VksUsersOther { get; set; }
    }
}
