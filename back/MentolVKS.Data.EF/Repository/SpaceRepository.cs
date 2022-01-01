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
    public class SpaceRepository : TableBasedEntityRepositoryBase<Space>, ISpaceRepository
    {
        public SpaceRepository(DataContext context, IColumnMappingConfiguration mappings) : base(context, mappings)
        {
        }

        public async Task<Space> GetByUriAsync(string item)
        {
            return await DbSet.FirstOrDefaultAsync(c => c.Uri == item);
        }

        public async Task<Space> GetByUriAltAsync(string item)
        {
            return await DbSet.FirstOrDefaultAsync(c => c.UriAlt == item && c.UriAlt!="" && c.UriAlt!=null);
        }
        
        public async Task<Space> GetByCallIdAsync(string item)
        {
            return await DbSet.FirstOrDefaultAsync(c => c.CallId == item && c.CallId!="" && c.CallId!=null);
        }

        public async Task<List<SelectDirectoryView>> GetSelectDirectoryAsync(string search, int limit)
        {
            return await DbSet
                       .Where(c=>c.IsAvailableForBooking==true)
                       .Select(c => new SelectDirectoryView
                       {
                          Id = c.Id,
                          Name = c.Name + " (" + c.CallId + ")"
                       })
                       .Where(c => c.Name.ToLower().Contains(search.ToLower()))
                       .Take(limit)
                       .OrderBy(c => c.Name)
                       .ToListAsync();
        }

        public async Task<List<Space>> GetByGuidAndGroupId(string guid, int? groupId)
        {
            return await DbSet.Where(c => c.Guid == guid && c.ServersGroupsId == groupId).ToListAsync();
        }

        public async Task<Space> GetFirstSpaceAsync()
        {
            return await DbSet.FirstOrDefaultAsync();
        }
    }
}
