using MentolVKS.Common.TypeExtensions;
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
    public class ConnectionTypeRepository : TableBasedEntityRepositoryBase<ConnectionType>, IConnectionTypeRepository
    {
        public ConnectionTypeRepository(DataContext context, IColumnMappingConfiguration mappings) : base(context, mappings)
        {
        }

        public async Task<List<SelectDirectoryView>> GetSelectDirectoryAsync(string search, int limit)
        {
            return await DbSet
                .Where(c => c.Name.ToLower().Contains(search.ToLower()))
                .Take(limit)
                .OrderBy(c => c.Name)
                .Select(c => new SelectDirectoryView
                {
                    Id = c.Id,
                    Name = c.Name,
                    PrivateName = c.PrivateName
                })
                .ToListAsync();
        }
    }
}
