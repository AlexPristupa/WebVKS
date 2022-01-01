using System.Collections.Generic;

namespace MentolVKS.Models
{
    /// <summary>
    /// Модель данных для Get-запросов.
    /// </summary>
    public class BookingFullViewModel : BookingPutViewModel
    {
        /// <summary>
        /// Участники.
        /// </summary>
        public new List<LinkBookingToParticipantViewModel> LinkBookingToParticipants { get; set; } = new List<LinkBookingToParticipantViewModel>();
    }
}
