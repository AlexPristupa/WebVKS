using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeZone = MentolVKS.Model.BaseModel.TimeZone;

namespace MentolVKS.Service.Contract
{
    public partial interface IService
    {
        /// <summary>
        /// Получить TimeZone для select
        /// </summary>
        /// <param name="search"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        Task<List<TimeZone>> GetTimeZoneSelectDirectoryAsync(string search, int limit);

        Task<TimeZone> GetTimeZoneByIdAsync(int id);
    }
}
