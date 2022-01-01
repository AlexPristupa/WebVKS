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
    public class FilterColumnRepository : ViewBasedEntityRepositoryBase<FilterColumn>, IFilterColumnRepository
    {
        public FilterColumnRepository(DataContext context, IColumnMappingConfiguration mappings) : base(context, mappings)
        {
        }

        public async Task<IEnumerable<FilterColumn>> AllByFilterIdAsync(int filterId)
        {
            var sql = @"select distinct
                                fcl.idr as Id,
                                fcl.ColumnName as ColumnName,
                                fctl.TypeId as TypeId,
                                fctl.TypeName as TypeName
                                from dbo.FilterColumnsList fcl
                                inner join dbo.FilterValue fv on fv.ColumnId = fcl.idr
                                inner join dbo.FilterForColumnTypeList fctl on fctl.idr = fcl.FilterTypeId
                                inner join dbo.FiltersList fl on fl.idr = fv.FilterId
                                where fl.idr = @p0";

            return await DbSet.FromSqlRaw(sql, CreateParameter("@p0", filterId)).ToListAsync();
        }
    }
}
