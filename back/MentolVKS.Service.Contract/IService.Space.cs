using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MentolVKS.Service.Contract
{
    public partial interface IService
    {
        /// <summary>
        /// Добавить комнату
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<Space> AddSpaceAsync(Space item);
        /// <summary>
        /// Обновить комнату
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<Space> UpdateSpaceAsync(Space item);
        /// <summary>
        /// Удалить комнату
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteSpaceAsync(int id);
        /// <summary>
        /// Получить комнату по ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Space> GetSpaceByIdAsync(int id);
        /// <summary>
        /// Справочник комнат для select
        /// </summary>
        /// <param name="search"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        Task<List<SelectDirectoryView>> GetSpaceSelectDirectoryAsync(string search, int limit);
       
    }
}
