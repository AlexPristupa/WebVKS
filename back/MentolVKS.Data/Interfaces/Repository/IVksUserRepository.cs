using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MentolVKS.Data.Interfaces.Repository
{
    public interface IVksUserRepository : IRepository<VksUser>
    {
        Task<List<SelectDirectoryView>> GetSelectDirectoryAsync(string search, int limit);
        Task<VksUser> GetByJidAsync(string jid);
    }
}
