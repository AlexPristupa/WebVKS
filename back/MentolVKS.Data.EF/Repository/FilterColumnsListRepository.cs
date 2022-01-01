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
    public class FilterColumnsListRepository : TableBasedEntityRepositoryBase<FilterColumnsList>, IFilterColumnsListRepository
    {
        public FilterColumnsListRepository(DataContext context, IColumnMappingConfiguration mappings) : base(context, mappings)
        {
        }

        /// <inheritdoc />
        public async Task<IEnumerable<FilterColumnsList>> AllByTableAsync(string tableName)
        {
            return await DbSet.Where(f => f.Table.TableName == tableName).OrderBy(f => f.Title).ToListAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<FilterColumnsList>> GetColumnValueFromTableAsync(int filterId, string colName, string tableName)
        {
            const string sql = @"select distinct fcl.*
                                from dbo.FiltersList fl
                                inner join dbo.FilterValue fv on fv.FilterId = fl.idr
                                inner join dbo.FilterColumnsList fcl on fcl.idr = fv.ColumnId
                                inner join dbo.FilterTablesList ftl on ftl.idr = fcl.TableId
                                where fl.idr = @p0 and upper(fcl.ColumnName) = upper(@p1) and upper(ftl.DBTable) = upper(@p2)";

            var filterColumnList = DbSet.FromSqlRaw(sql, filterId, colName, tableName);

            return await filterColumnList.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<FilterColumnsList>> GetByColumnAndTableAsync(string columnName, string tableName)
        {
            const string sql = @"SELECT fcl.*
                                FROM dbo.FilterTablesList ftl
                                INNER JOIN dbo.FilterColumnsList AS fcl ON ftl.idr = fcl.TableId
                                where upper(ftl.TableName) = upper(@p0) and upper(fcl.ColumnName) = upper(@p1)";
            var filterColumnList = await DbSet.FromSqlRaw(sql, tableName, columnName).ToListAsync();

            return filterColumnList;
        }

        public async Task<IEnumerable<FilterColumnsList>> GetByFilterAndTableAsync(int filterId, string tableName)
        {
            const string sql = @"select distinct fcl.*
                                from dbo.FiltersList fl
                                inner join dbo.FilterValue fv on fv.FilterId = fl.idr
                                inner join dbo.FilterColumnsList fcl on fcl.idr = fv.ColumnId
                                inner join dbo.FilterTablesList ftl on ftl.idr = fcl.TableId
                                where fl.idr = @p0 and upper(ftl.TableName) = upper(@p1)";
            var filterColumnList = await DbSet.FromSqlRaw(sql, filterId, tableName).ToListAsync();

            return filterColumnList;
        }
    }
}
