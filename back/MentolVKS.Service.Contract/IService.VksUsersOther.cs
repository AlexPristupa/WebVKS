using MentolVKS.Model.BaseModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MentolVKS.Service.Contract
{
    public partial interface IService
    {
        /// <summary>
        /// Получает справочник внешних участников
        /// </summary>
        /// <param name="search">Значение для поиска</param>
        /// <param name="limit">Ограничение на количество записей</param>
        /// <returns>Справочник из идентификатора и имени внешнего участника</returns>
        Task<List<SelectDirectoryView>> GetVksUsersOtherSelectDirectoryAsync(string search, int limit);
        
        /// <summary>
        /// Получить VksUserOther по id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<VksUsersOther> GetVksUsersOtherByIdAsync(int id);
    }
}
