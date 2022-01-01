using MentolVKS.Model.Bases;
using MentolVKS.Model.Filters.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MentolVKS.Data.Interfaces.Repository
{
    public interface IAgGridVueRepository : IRepository<TableBasedEntityBase>
    {
        /// <summary>
        /// Возвращает записи по DTO
        /// </summary>
        /// <param name="listQuery">DTO</param>
        /// <param name="listFields">Поля быстрого поиска по значению TableSearchBy для построения UNION ALL</param>
        Task<dynamic> GetDataAsync(FilterQuery listQuery, IEnumerable<string> listFields);

        /// <summary>
        ///     Возвращает соответсвие таблиц в базе данных с представлениеям во фронте и сущностями
        /// </summary>
        /// <returns></returns>
        IEnumerable<ITableNameToModel> GetModelFactoryList();

        /// <summary>
        /// Получить модель по имени таблицы
        /// </summary>
        /// <param name="tableName">Имя таблицы</param>
        /// <returns>Модель фабрики</returns>
        ITableNameToModel GetModelFactory(string tableName);
    }
}
