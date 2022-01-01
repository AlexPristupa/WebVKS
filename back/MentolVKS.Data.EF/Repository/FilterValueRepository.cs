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
    public class FilterValueRepository : TableBasedEntityRepositoryBase<FilterValue>, IFilterValueRepository
    {
        public FilterValueRepository(DataContext context, IColumnMappingConfiguration mappings) : base(context, mappings)
        {
        }
        public async Task<IEnumerable<FilterValue>> AllByFilterIdAsync(int filterId)
        {
            return await DbSet.Where(v => v.FilterId == filterId).ToListAsync();
        }

        public async Task<IEnumerable<FilterValue>> AllByFilterAndColumnAsync(int filterId, int columnId)
        {
            return await DbSet.Where(v => v.FilterId == filterId && v.ColumnId == columnId).ToListAsync();
        }
    }
}
