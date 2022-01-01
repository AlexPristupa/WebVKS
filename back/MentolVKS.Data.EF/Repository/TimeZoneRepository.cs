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
    public class TimeZoneRepository : TableBasedEntityRepositoryBase<Model.BaseModel.TimeZone>, ITimeZoneRepository
    {
        public TimeZoneRepository(DataContext context, IColumnMappingConfiguration mappings) : base(context, mappings)
        {
        }

        public async Task<Model.BaseModel.TimeZone> GetByStandartId(string id)
        {
            return await DbSet.FirstOrDefaultAsync(c => c.StandartId == id);
        }

        public async Task<List<Model.BaseModel.TimeZone>> GetSelectDirectoryAsync(string search, int limit)
        {
            return await DbSet
                .Where(c => c.PrivateName.ToLower().Contains(search.ToLower()))
                .Take(limit)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<Model.BaseModel.TimeZone> GetTimeZoneByOffset(int minute)
        {
            return await DbSet.FirstOrDefaultAsync(c => c.OffsetMinute == minute);
        }
    }
}
