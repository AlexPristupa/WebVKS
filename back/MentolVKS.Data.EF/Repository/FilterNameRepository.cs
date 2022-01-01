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
    public class FilterNameRepository : ViewBasedEntityRepositoryBase<FilterName>, IFilterNameRepository
    {
        public FilterNameRepository(DataContext context, IColumnMappingConfiguration mappings) : base(context, mappings)
        {
        }

        public async Task<IEnumerable<FilterName>> AllByTableAndUserIdAsync(string tableName, int userId)
        {
            const string sql = @"select 
                                fl.idr as Id, 
                                fl.FilterName as Name, 
                                ful.IsActive,
                                fl.IsCommon
                                from dbo.FiltersToUserLink ful
                                left join dbo.FiltersList fl on fl.idr = ful.FilterId
                                inner join 
                                (select fv.FilterId as idr
                                from dbo.FilterTablesList ftl
                                left join dbo.FilterColumnsList fcl on fcl.TableId = ftl.idr
                                left join dbo.FilterValue fv on fv.ColumnId = fcl.idr
                                where ftl.TableName = @p0
                                group by fv.FilterId) as temp 
                                on temp.idr = fl.idr
                                where UserId = @p1 or IsCommon = 1";

            var row = await DbSet.FromSqlRaw(sql, tableName, userId).ToListAsync();

            return row;
        }
    }
}
