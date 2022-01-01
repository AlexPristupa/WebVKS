using MentolVKS.Data.EF.Bases;
using MentolVKS.Data.Interfaces;
using MentolVKS.Data.Interfaces.Repository;
using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MentolVKS.Data.EF.Repository
{
    public class FilterTablesListRepository : TableBasedEntityRepositoryBase<FilterTablesList>, IFilterTablesListRepository
    {
        public FilterTablesListRepository(DataContext context, IColumnMappingConfiguration mappings) : base(context, mappings)
        {
        }
        public async Task<FilterTablesList> GetByTableAsync(string tableName)
        {
            return await DbSet.FirstOrDefaultAsync(t => t.TableName == tableName);
        }
    }
}
