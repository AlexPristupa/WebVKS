using MentolVKS.Model.BaseModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MentolVKS.Data.Interfaces.Repository
{
    /// <summary>
    /// Интерфейс репозитория для объектов типа <see cref="AspNetUser"/>
    /// </summary>
    public interface IAspNetUserRepository : IRepository<AspNetUser>
    {
        Task<AspNetUser> GetByNameAsync(string name);
        Task SetUserRlsAsync(int userId);
        Task SetLdapUserDefaultRlsAsync(int userId);
        bool CheckUserPassword(string password, string hashPassword);
        /// <summary>
        /// Получает справочник
        /// </summary>
        /// <param name="search">Значение для поиска</param>
        /// <param name="limit">Ограничение на количество записей</param>
        /// <returns>Справочник из идентификатора и имени пользователя</returns>
        Task<List<SelectDirectoryView>> GetSelectDirectoryAsync(string search, int limit);
    }
}
