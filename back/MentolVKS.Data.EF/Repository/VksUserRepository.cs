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
    public class VksUserRepository : TableBasedEntityRepositoryBase<VksUser>, IVksUserRepository
    {
        public VksUserRepository(DataContext context, IColumnMappingConfiguration mappings) : base(context, mappings)
        {
        }

        public async Task<List<SelectDirectoryView>> GetSelectDirectoryAsync(string search, int limit)
        {
            return await DbSet                
                .Select(c => new SelectDirectoryView
                {
                    Id = c.Id,
                    Name = c.Name+" ("+c.JID+")"
                })
                .Where(c => c.Name.ToLower().Contains(search.ToLower()))
                .OrderBy(c => c.Name)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<VksUser> GetByJidAsync(string jid)
        {
            return await DbSet.FirstOrDefaultAsync(c => c.JID.ToLower().StartsWith(jid.ToLower()));
        }
    }
}
