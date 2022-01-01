using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MentolVKS.Data.Interfaces.Repository
{
    public interface IFilterColumnsListRepository : IRepository<FilterColumnsList>
    {
        /// <summary>
        /// Возвращает фильтры колонок по таблице
        /// </summary>
        /// <param name="tableName">Имя таблицы</param>
        /// <returns>Список фильтров колонок</returns>
        Task<IEnumerable<FilterColumnsList>> AllByTableAsync(string tableName);
        Task<IEnumerable<FilterColumnsList>> GetColumnValueFromTableAsync(int filterId, string colName, string tableName);
        Task<IEnumerable<FilterColumnsList>> GetByColumnAndTableAsync(string columnName, string tableName);
        Task<IEnumerable<FilterColumnsList>> GetByFilterAndTableAsync(int filterId, string tableName);
    }
}
