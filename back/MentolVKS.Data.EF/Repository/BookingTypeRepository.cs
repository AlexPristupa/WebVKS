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
    public class BookingTypeRepository : TableBasedEntityRepositoryBase<BookingType>, IBookingTypeRepository
    {
        public BookingTypeRepository(DataContext context, IColumnMappingConfiguration mappings) : base(context, mappings)
        {
        }

        public async Task<List<BookingType>> GetSelectDirectoryAsync(string search, int limit)
        {
            return await DbSet
                       .Where(c => c.PrivateName.ToUpper().Contains(search.ToUpper()))
                       .Take(limit)
                       .OrderBy(c => c.Name)
                       .ToListAsync();
        }
    }
}
