using System.Collections.Generic;
using System.Threading.Tasks;
using MentolVKS.Model.BaseModel;

namespace MentolVKS.Data.Interfaces.Repository
{
    /// <summary>
    /// Интерфейс репозитория для объектов типа <see cref="LinkBookingToParticipant"/>.
    /// </summary>
    public interface ILinkBookingToParticipantRepository : IRepository<LinkBookingToParticipant>
    {
        /// <summary>
        /// Возвращает связи с участниками для бронирования.
        /// </summary>
        /// <param name="bookingId">Идентификатор бронирования.</param>
        /// <returns>Асинхронная задача с результатом типа <see cref="List{LinkBookingToParticipant}"/>.</returns>
        Task<List<LinkBookingToParticipant>> AllByBookingIdAsync(int bookingId);
    }
}
