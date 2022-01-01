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
    public class TableColumnSettingsRepository : TableBasedEntityRepositoryBase<TableColumnSettings>, ITableColumnSettingsRepository
    {
        public TableColumnSettingsRepository(DataContext context, IColumnMappingConfiguration mappings) : base(context, mappings)
        {
        }

        public async Task<IEnumerable<TableColumnSettings>> GetByTableNameAsync(string tableName)
        {
            return await DbSet.AsNoTracking().Where(t => t.TableName == tableName).ToListAsync();
        }
    }
}
