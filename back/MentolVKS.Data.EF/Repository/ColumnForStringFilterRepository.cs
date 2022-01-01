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
    internal class ColumnForStringFilterRepository : ViewBasedEntityRepositoryBase<ColumnForStringFilter>, IColumnForStringFilterRepository
    {
        public ColumnForStringFilterRepository(DataContext context, IColumnMappingConfiguration mappings) : base(context, mappings)
        {
        }

        public async Task<IEnumerable<ColumnForStringFilter>> AllByQueryAsync(string sql, FilterParametrsDto columnFilter, string[] values, params object[] parameters)
        {
            var param = new List<object>(parameters);

            if (values != null && values.Any())
            {
                var sqlValues = string.Join(",", values.Select(v => $"'{v}'"));

                if (sql.IndexOf("ilike '%%'") > 0)
                {
                    sql = sql.Replace("ilike '%%'", $"in ({sqlValues})");
                }
                else
                {
                    sql = sql.Replace("like '%%'", $"in ({sqlValues})");
                }
            }

            sql = sql.Replace("{" + param.Count + "}", "null or 1=1");

            if (columnFilter != null)
            {
                var index = param.Count;
                var columnReg = new Regex(@"{\w+\=((\w+\.)?\w+)\}");
                var keyWordReg = new Regex("^(where|and)", RegexOptions.IgnoreCase);

                foreach (var columnFilters in columnFilter.ColumnFilters)
                {
                    var conditionReg = new Regex(@"(where|and)\s*\{" + columnFilters.ColumnName + @"\=(\w+\.)?\w+\}\s*\{operand\}\s*\{value\}", RegexOptions.IgnoreCase);

                    if (!conditionReg.IsMatch(sql)) continue;

                    var condition = conditionReg.Match(sql).Value;

                    var keyWord = string.Empty;

                    var columnCondition = keyWordReg.Replace(condition, string.Empty);
                    var resultCondition = string.Empty;

                    if (columnFilters.StringSearch != null)
                    {
                        foreach (var filter in columnFilters.StringSearch)
                        {
                            switch (filter.Operand)
                            {
                                case "~":
                                    resultCondition += keyWord + columnReg.Replace(columnCondition, "$1").Replace("{operand}", " like ").Replace("{value}", "{" + index + "}");
                                    break;
                                case "!~":
                                    resultCondition += keyWord + columnReg.Replace(columnCondition, "$1").Replace("{operand}", " not like ").Replace("{value}", "{" + index + "}");
                                    break;
                                case "=":
                                    resultCondition += keyWord + columnReg.Replace(columnCondition, "$1").Replace("{operand}", "=").Replace("{value}", "{" + index + "}");
                                    break;
                                case "!=":
                                    resultCondition += keyWord + columnReg.Replace(columnCondition, "$1").Replace("{operand}", "!=").Replace("{value}", "{" + index + "}");
                                    break;
                                case "~*":
                                    resultCondition += keyWord + columnReg.Replace(columnCondition, "$1").Replace("{operand}", " like ").Replace("{value}", "{" + index + "}+'%'");
                                    break;
                                case "!~*":
                                    resultCondition += keyWord + columnReg.Replace(columnCondition, "$1").Replace("{operand}", " not like ").Replace("{value}", "'%'+{" + index + "}");
                                    break;
                                case "*~":
                                    resultCondition += keyWord + columnReg.Replace(columnCondition, "$1").Replace("{operand}", " like ").Replace("{value}", "'%'+{" + index + "}");
                                    break;
                                case "*!~":
                                    resultCondition += keyWord + columnReg.Replace(columnCondition, "$1").Replace("{operand}", " not like ").Replace("{value}", "{" + index + "}+'%'");
                                    break;
                                case "*~*":
                                    resultCondition += keyWord + columnReg.Replace(columnCondition, "$1").Replace("{operand}", " like ").Replace("{value}", "'%'+{" + index + "}+'%'");
                                    break;
                                case "!*~*":
                                    resultCondition += keyWord + columnReg.Replace(columnCondition, "$1").Replace("{operand}", " not like ").Replace("{value}", "'%'+{" + index + "}+'%'");
                                    break;
                                default:
                                    resultCondition += keyWord + columnReg.Replace(columnCondition, "$1").Replace("{operand}", "=").Replace("{value}", "{" + index + "}");
                                    break;
                            }

                            param.Add(filter.StrSearch);

                            keyWord = " or ";

                            index++;
                        }
                    }

                    sql = sql.Replace(columnCondition, resultCondition);
                }
            }

            var lastReg = new Regex(@"(where|and)\s*\{\w+\=(\w+\.)?\w+\}\s*\{operand\}\s*\{value\}", RegexOptions.IgnoreCase);

            sql = lastReg.Replace(sql, string.Empty).Replace("vc.id", "cast(vc.id as bigint) as id");

            var dbSet = DbSet.FromSqlRaw(sql, param.ToArray());

            return await EFLinqFromParametrs(columnFilter, dbSet).ToListAsync();
        }

        public async Task<IEnumerable<ColumnForStringFilter>> AllByTableAndColumnAsync(string tableName, string columnName, DateTime? dateFrom, DateTime? dateTo, FilterParametrsDto columnFilter)
        {
            var dateCondition = dateFrom == null || dateTo == null
   ? string.Empty
   : "where DateStart between '{0}' and '{1}'";

            var sql = $@"select vc.id, vc.value, '' state
                    from (select ROW_NUMBER() OVER(ORDER BY {columnName} ASC) as id, {columnName} as value
                    from dbo.{tableName} {dateCondition}
                    group by {columnName}) vc ";

            var dbSet = DbSet.FromSqlRaw(sql, dateFrom, dateTo);

            return await EFLinqFromParametrs(columnFilter, dbSet).ToListAsync();
        }

        private static IQueryable<ColumnForStringFilter> EFLinqFromParametrs(FilterParametrsDto columnFilter, IQueryable<ColumnForStringFilter> dbSet)
        {
            // Если не нужна выборка
            if (!string.IsNullOrEmpty(columnFilter.Search))
                dbSet = dbSet.Where(c => Microsoft.EntityFrameworkCore.EF.Functions.Like(c.Value, $"%{columnFilter.Search}%"));

            // Если нужна ограниченная выборка
            if (columnFilter.Limit != 0)
                dbSet = dbSet.AsNoTracking().AsQueryable().Take(columnFilter.Limit);

            return dbSet;
        }
    }
}
