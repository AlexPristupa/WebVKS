using MentolVKS.Data.EF.Bases;
using MentolVKS.Data.Interfaces;
using MentolVKS.Data.Interfaces.Repository;
using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentolVKS.Data.EF.Repository
{
    public class AspNetRoleRepository : TableBasedEntityRepositoryBase<AspNetRole>, IAspNetRoleRepository
    {
        public AspNetRoleRepository(DataContext context, IColumnMappingConfiguration mappings) : base(context, mappings)
        {
        }

        public async Task<List<AspNetRole>> GetByUserIdAsync(int userId)
        {
            return await DbSet.Include(c => c.AspNetUserRoles).Where(c => c.AspNetUserRoles.Where(d => d.UserId == userId).ToList().Count>0).ToListAsync();
        }
    }
}
