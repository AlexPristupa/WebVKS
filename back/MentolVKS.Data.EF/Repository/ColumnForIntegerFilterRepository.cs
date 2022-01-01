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
    internal class ColumnForIntegerFilterRepository : ViewBasedEntityRepositoryBase<ColumnForIntegerFilter>, IColumnForIntegerFilterRepository
    {
        public ColumnForIntegerFilterRepository(DataContext context, IColumnMappingConfiguration mappings) : base(context, mappings)
        {
        }

        public async Task<IEnumerable<ColumnForIntegerFilter>> GetFromSqlAsync(string sql)
        {
            return await DbSet.FromSqlRaw(sql).ToListAsync();
        }
    }
}
