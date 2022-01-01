using MentolVKS.Model.BaseModel;
using MentolVKS.Model.Bases;
using MentolVKS.Model.Filters.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MentolVKS.Data.Interfaces.Repository
{
    public interface IColumnForStringFilterRepository : IRepository<ColumnForStringFilter>
    {
        /// <summary>
        /// Возвращает данные фильтра по колонке и таблице
        /// </summary>
        /// <returns>Данные фильтра</returns>
        Task<IEnumerable<ColumnForStringFilter>> AllByTableAndColumnAsync(string tableName, string columnName, DateTime? dateFrom, DateTime? dateTo, FilterParametrsDto columnFilter);
        /// <summary>
        /// Возвращает данные фильтра по запросу
        /// </summary>
        /// <param name="sql">SQL-запрос</param>
        /// <param name="columnFilter">Данные фильтра</param>
        /// <param name="values">Отображаемые значения</param>
        /// <param name="parameters">Параметры запроса</param>
        /// <returns>Данные фильтра</returns>
        Task<IEnumerable<ColumnForStringFilter>> AllByQueryAsync(string sql, FilterParametrsDto columnFilter, string[] values, params object[] parameters);
    }
}
