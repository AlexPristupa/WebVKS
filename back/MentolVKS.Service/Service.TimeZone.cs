using MentolVKS.Data.Interfaces;
using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using MentolVKS.Service.Contract;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeZone = MentolVKS.Model.BaseModel.TimeZone;

namespace MentolVKS.Service
{
    public partial class Service : IService
    {
       public async Task<List<TimeZone>> GetTimeZoneSelectDirectoryAsync(string search, int limit)
        {
            if (search == null) search = string.Empty;

            return await UnitOfWork.TimeZoneRepository.GetSelectDirectoryAsync(search, limit);
        }
        public async Task<TimeZone> GetTimeZoneByIdAsync(int id)
        {
            return await UnitOfWork.TimeZoneRepository.GetByIdAsync(id);
        }
    }
}