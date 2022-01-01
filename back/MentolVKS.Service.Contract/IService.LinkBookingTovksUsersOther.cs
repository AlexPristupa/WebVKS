using System.Threading.Tasks;

namespace MentolVKS.Service.Contract
{
    public partial interface IService
    {
        /// <summary>
        /// Проверяет уникальность ссылки на участника.
        /// </summary>
        /// <param name="uri">Ссылка на участника.</param>
        /// <param name="bookingId">Идентификатор бронирования.</param>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Асинхронная задача с реузльтатом <see cref="bool"/>: <see langword="true"/> - строка уникальна, <see langword="false"/> - строка не уникальна.</returns>
        Task<bool> LinkBookingTovksUsersOtherCheckUriAsync(string uri, int userId);
    }
}
