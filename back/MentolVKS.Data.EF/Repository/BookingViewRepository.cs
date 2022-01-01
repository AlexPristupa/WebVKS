using MentolVKS.Data.EF.Bases;
using MentolVKS.Data.Interfaces;
using MentolVKS.Data.Interfaces.Repository;
using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using MentolVKS.Model.Filters.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MentolVKS.Data.EF.Repository
{
    internal class BookingViewRepository : ViewBasedEntityRepositoryBase<BookingView>, IBookingViewRepository
    {
        public BookingViewRepository(DataContext context, IColumnMappingConfiguration mappings) : base(context, mappings)
        {
        }

        public async Task<List<BookingView>> GetBySpaceIdAsync(int spaceId)
        {
            return await DbSet.Where(c => c.SpaceId == spaceId).ToListAsync();
        }
    }
}
