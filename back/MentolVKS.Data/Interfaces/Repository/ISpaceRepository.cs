using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MentolVKS.Data.Interfaces.Repository
{
    public interface ISpaceRepository : IRepository<Space>
    {
        Task<Space> GetByUriAsync(string item);
        Task<Space> GetByUriAltAsync(string item);
        Task<Space> GetByCallIdAsync(string item);
        Task<List<SelectDirectoryView>> GetSelectDirectoryAsync(string search, int limit);
        Task<List<Space>> GetByGuidAndGroupId(string guid, int? groupId);
        Task<Space> GetFirstSpaceAsync();
    }
}
