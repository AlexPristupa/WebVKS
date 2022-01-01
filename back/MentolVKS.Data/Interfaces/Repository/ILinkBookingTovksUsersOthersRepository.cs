using System.Collections.Generic;
using System.Threading.Tasks;
using MentolVKS.Model.BaseModel;

namespace MentolVKS.Data.Interfaces.Repository
{
    /// <summary>
    /// Интерфейс репозитория для объектов типа <see cref="LinkBookingTovksUsersOther"/>.
    /// </summary>
    public interface ILinkBookingTovksUsersOthersRepository : IRepository<LinkBookingTovksUsersOther>
    {
        /// <summary>
        /// Возвращает список участников для бронирования.
        /// </summary>
        /// <param name="bookingId">Идентификатор бронирования.</param>
        /// <returns>Список внешних участников.</returns>
        Task<List<LinkBookingTovksUsersOther>> AllByBookingAsync(int bookingId);

        /// <summary>
        /// Возвращает список участников с заданным URI.
        /// </summary>
        /// <param name="uri">Ссылка на участника.</param>
        /// <returns>Список внешних участников.</returns>
        Task<IEnumerable<LinkBookingTovksUsersOther>> AllByUriAsync(string uri);
    }
}
