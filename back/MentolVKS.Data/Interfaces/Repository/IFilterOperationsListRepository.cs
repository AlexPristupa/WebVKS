using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MentolVKS.Data.Interfaces.Repository
{
    public interface IFilterOperationsListRepository : IRepository<FilterOperationsList>
    {
        Task<FilterOperationsList> GetByOperandAsync(string operand);
    }
}
