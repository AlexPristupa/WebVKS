using MentolVKS.Model.BaseModel;
using MentolVKS.Model.Bases;
using MentolVKS.Model.Filters.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MentolVKS.Data.Interfaces.Repository
{
    public interface IColumnForIntegerFilterRepository : IRepository<ColumnForIntegerFilter>
    {
        Task<IEnumerable<ColumnForIntegerFilter>> GetFromSqlAsync(string sql);
    }
}
