using MentolVKS.Data.Interfaces;
using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using MentolVKS.Service.Contract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MentolVKS.Service
{
    public partial class Service : IService
    {
       public async Task<List<PinPolitics>> GetPinPoliticsSelectDirectoryAsync(string search, int limit)
        {
            return await UnitOfWork.PinPoliticsRepository.GetSelectDirectoryAsync(search, limit);
        }
    }
}