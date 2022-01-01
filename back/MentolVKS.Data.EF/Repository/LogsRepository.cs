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
    public class BookingRepository : TableBasedEntityRepositoryBase<Booking>, IBookingRepository
    {
        public BookingRepository(DataContext context, IColumnMappingConfiguration mappings) : base(context, mappings)
        {
        }

        public async Task<List<Booking>> GetBySpaceIdAsync(int spaceId)
        {
            return await DbSet.Where(c => c.SpaceId==spaceId).ToListAsync();
        }

        public async Task<Booking> GetByUidAsync(string uid)
        {
            return await DbSet.FirstOrDefaultAsync(c => c.Uid == uid);
        }
    }
}
