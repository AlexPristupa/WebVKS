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
       public async Task<List<SelectDirectoryView>> GetVksServerSelectDirectoryAsync(string search, int limit)
        {
            return await UnitOfWork.VksServerRepository.GetSelectDirectoryAsync(search, limit);
        }
    }
}