using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MentolVKS.Data.Interfaces.Repository
{
    public interface IFilterForColumnTypeListRepository : IRepository<FilterForColumnTypeList>
    {
        Task<IEnumerable<FilterForColumnTypeList>> GetTypeFilterAsync(int filterId, string colName, string tableName);
    }
}
