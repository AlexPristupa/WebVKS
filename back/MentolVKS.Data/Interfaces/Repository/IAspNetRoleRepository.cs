using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MentolVKS.Data.Interfaces.Repository
{
    public interface IAspNetRoleRepository : IRepository<AspNetRole>
    {
        Task<List<AspNetRole>> GetByUserIdAsync(int userId);
    }
}
