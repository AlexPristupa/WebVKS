using MentolVKS.Model.BaseModel;
using MentolVKS.Model.Bases;
using MentolVKS.Model.Filters.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MentolVKS.Data.Interfaces.Repository
{
    public interface IFilterColumnRepository : IRepository<FilterColumn>
    {
        Task<IEnumerable<FilterColumn>> AllByFilterIdAsync(int filterId);
    }
}
