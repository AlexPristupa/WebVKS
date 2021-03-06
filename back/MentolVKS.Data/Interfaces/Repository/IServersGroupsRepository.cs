using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MentolVKS.Data.Interfaces.Repository
{
    public interface IServersGroupsRepository : IRepository<ServersGroups>
    {
        Task<List<SelectDirectoryView>> GetSelectDirectoryAsync(string search, int limit);
    }
}
