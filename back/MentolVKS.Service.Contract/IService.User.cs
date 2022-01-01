using MentolVKS.Model.BaseModel;
using MentolVKS.Model.Enums;
using MentolVKS.Model.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MentolVKS.Service.Contract
{
    public partial interface IService
    {
        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="Provider"></param>
        /// <returns></returns>
        Task<IdentityModel> AuthUserAsync(string login, string password, Provider Provider);
        /// <summary>
        /// Получить токен
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IdentityModel> GetRefreshTokenAsync(string token);
        /// <summary>
        /// Сохранить токен
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ip"></param>
        /// <param name="fingerPrint"></param>
        /// <returns></returns>
        Task<string> SaveRefreshTokenAsync(int userId, int idleMinute, string fingerPrint, string ip);
        /// <summary>
        /// Удалить refreshToken
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task DeleteRefreshTokenAsync(string token);
        Task<AspNetUser> GetUserByIdAsync(int id);
        Task<AspNetUser> GetUserByLoginAsync(string login);
        Task<List<AspNetRole>> GetUserRolesAsync(int userId);
        Task<List<string>> GetUserRolesLicenseAsync(int userId);
        Task<AspNetUser> CreateUserAsync(string login, string passwrod, string fullName, string email, string post, Provider Provider,bool auto=false);
        Task<AspNetUser> UpdateUserAsync(AspNetUser item);
        Task DeleteUserAsync(int id);
        Task ChangeUserRoles(int userId, List<int> roles);
        Task SetRls(int userId);
        Task SetLdapUserDefaultRlsAsync(int userId);

        /// <summary>
        /// Получает справочник
        /// </summary>
        /// <param name="search">Значение для поиска</param>
        /// <param name="limit">Ограничение на количество записей</param>
        /// <returns>Справочник из идентификатора и имени пользователя</returns>
        Task<List<SelectDirectoryView>> AspNetUserGetSelectDirectoryAsync(string search, int limit);
    }
}
