using MentolVKS.Model.BaseModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MentolVKS.Service.Contract
{
    public partial interface IService
    {
        /// <summary>
        /// Получить VksUser для select
        /// </summary>
        /// <param name="search"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        Task<List<SelectDirectoryView>> GetVksUserSelectDirectoryAsync(string search, int limit);

        /// <summary>
        /// Получает участника по jid
        /// </summary>
        /// <param name="jid"></param>
        /// <returns></returns>
        Task<VksUser> GetVksUserByJidAsync(string jid);

        /// <summary>
        /// Получает участника по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns></returns>
        Task<VksUser> GetVksUserByIdAsync(int id);
    }
}
