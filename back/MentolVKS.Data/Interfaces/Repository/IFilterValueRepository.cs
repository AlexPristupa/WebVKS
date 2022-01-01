using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MentolVKS.Data.Interfaces.Repository
{
    public interface IFilterValueRepository : IRepository<FilterValue>
    {
        Task<IEnumerable<FilterValue>> AllByFilterIdAsync(int filterId);
        Task<IEnumerable<FilterValue>> AllByFilterAndColumnAsync(int filterId, int columnId);
    }
}
