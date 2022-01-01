using MentolVKS.Model.BaseModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MentolVKS.Data.Interfaces.Repository
{
    /// <summary>
    /// Интерфейс репозитория для объектов типа <see cref="VksUsersOther"/>
    /// </summary>
    public interface IVksUsersOtherRepository : IRepository<VksUsersOther>
    {
        /// <summary>
        /// Получает справочник внешних участников
        /// </summary>
        /// <param name="search">Значение для поиска</param>
        /// <param name="limit">Ограничение на количество записей</param>
        /// <returns>Справочник из идентификатора и имени внешнего участника</returns>
        Task<List<SelectDirectoryView>> GetSelectDirectoryAsync(string search, int limit);

        /// <summary>
        /// Возвращает внешнего участника по ссылке.
        /// </summary>
        /// <param name="uri">Ссылка на участника.</param>
        /// <returns>Асинхронная задача с данными участника.</returns>
        Task<VksUsersOther> GetByUriAsync(string uri);
        Task<VksUsersOther> GetByEmptyUriAndEmailAsync(string email);
        Task<VksUsersOther> GetByUriAndNotId(string uri, int id);
    }
}
