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
    public class FilterForColumnTypeListRepository : TableBasedEntityRepositoryBase<FilterForColumnTypeList>, IFilterForColumnTypeListRepository
    {
        public FilterForColumnTypeListRepository(DataContext context, IColumnMappingConfiguration mappings) : base(context, mappings)
        {
        }
        public async Task<IEnumerable<FilterForColumnTypeList>> GetTypeFilterAsync(int filterId, string colName, string tableName)
        {
            string sql = @"select fctl.* 
                                from dbo.FilterColumnsList fcl
                                inner join dbo.FilterTablesList ftl on ftl.idr = fcl.TableId
                                inner join dbo.FilterForColumnTypeList fctl on fctl.idr = fcl.FilterTypeId
                                where fcl.ColumnName = @p0 and ftl.TableName = @p1";

            if (Context.Database.IsNpgsql())
            {
                sql = @"select fctl.* 
                                from dbo.FilterColumnsList fcl
                                inner join dbo.FilterTablesList ftl on ftl.idr = fcl.TableId
                                inner join dbo.FilterForColumnTypeList fctl on fctl.idr = fcl.FilterTypeId
                                where fcl.ColumnName ilike @p0 and ftl.TableName ilike @p1";
            }

            var row = await DbSet.FromSqlRaw(sql, colName, tableName).ToListAsync();

            return row;
        }
    }
}
