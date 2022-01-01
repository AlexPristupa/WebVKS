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
    public class FiltersListRepository : TableBasedEntityRepositoryBase<FiltersList>, IFiltersListRepository
    {
        public FiltersListRepository(DataContext context, IColumnMappingConfiguration mappings) : base(context, mappings)
        {
        }
        public async Task<IEnumerable<FiltersList>> AllForTableByIdAsync(int filterId)
        {
            const string sql = @"select distinct fl2.*
                        from dbo.FiltersList fl
                        join dbo.FilterValue fv on fv.FilterId=fl.idr
                        join dbo.FilterColumnsList fcl on fcl.idr=fv.ColumnId
                        join dbo.FilterColumnsList fcl2 on fcl.TableId=fcl2.TableId
                        join dbo.FilterValue fv2 on fcl2.idr=fv2.ColumnId
                        join dbo.FiltersList fl2 on fv2.FilterId=fl2.idr
                        where fl.Idr={0}";

            return await DbSet.FromSqlRaw(sql, filterId).ToListAsync();
        }
    }
}
