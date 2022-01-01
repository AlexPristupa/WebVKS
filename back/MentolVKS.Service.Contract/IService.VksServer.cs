using MentolVKS.Model;
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
        Task<List<SelectDirectoryView>> GetVksServerSelectDirectoryAsync(string search, int limit);
    }
}
