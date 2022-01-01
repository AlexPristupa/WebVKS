using MentolVKS.Model.Filters.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MentolVKS.Data.Interfaces
{
    public interface IColumnFilters
    {
        /// <summary>
        /// Получить linq для колоночного фильтра типа Select
        /// </summary>
        /// <param name="query">LINQ запрос</param>
        /// <param name="item">Фильтр из шапки таблицы</param>
        /// <returns>IQueryable</returns>
        public IQueryable GetColumnFiltersTypeSelect(IQueryable query, FilterHandler item);

        /// <summary>
        /// Получить linq для колоночного фильтра типа Date
        /// </summary>
        /// <param name="query">LINQ запрос</param>
        /// <param name="item">Фильтр из шапки таблицы</param>
        /// <returns>IQueryable</returns>
        public IQueryable GetColumnFiltersTypeDate(IQueryable query, FilterHandler item);

        /// <summary>
        /// Получить linq для колоночного фильтра типа Time
        /// </summary>
        /// <param name="query">LINQ запрос</param>
        /// <param name="item">Фильтр из шапки таблицы</param>
        /// <returns>IQueryable</returns>
        public IQueryable GetColumnFiltersTypeTime(IQueryable query, FilterHandler item);

        /// <summary>
        /// Получить linq для колоночного фильтра типа String - Tree
        /// </summary>
        /// <param name="query">LINQ запрос</param>
        /// <param name="item">Фильтр из шапки таблицы</param>
        /// <returns>IQueryable</returns>
        public IQueryable GetColumnFiltersTypeStringTree(IQueryable query, FilterHandler item);

        /// <summary>
        /// Получить linq для колоночного фильтра типа Integer
        /// </summary>
        /// <param name="query">LINQ запрос</param>
        /// <param name="item">Фильтр из шапки таблицы</param>
        /// <returns>IQueryable</returns>
        public IQueryable GetColumnFiltersTypeInteger(IQueryable query, FilterHandler item);

        /// <summary>
        /// Получить linq для первичных ключей
        /// </summary>
        /// <param name="query">LINQ запрос</param>
        /// <param name="item">Фильтр из шапки таблицы</param>
        /// <returns>IQueryable</returns>
        public IQueryable GetColumnFiltersTypeSelectPk(IQueryable query, FilterHandler item);
    }
}
