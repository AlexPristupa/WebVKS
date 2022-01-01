using MentolVKS.Model.Filters.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MentolVKS.Data.Interfaces
{
    /// <summary>
    /// Интерфейс linq запроса для объектов типа <see cref="TEntity" />
    /// </summary>
    /// <typeparam name="TEntity">Сущность БД</typeparam>
    public interface IQueryLinq
    {
        /// <summary>
        /// Поличить НД отфильтрованный по дате
        /// </summary>
        /// <param name="listQuery">Фильтр</param>
        /// <param name="query">Запрос к БД</param>
        /// <returns></returns>
        IQueryable GetDateQuery(string field, FilterQuery filterQuery);

        /// <summary>
        /// Поличить НД отфильтрованный по текстовому полю
        /// </summary>
        /// <param name="excludeFields">Поля не участвующие в поиске</param>
        /// <param name="search">Значение для поиска</param>
        /// <param name="listFields">Список полей для фильтрации</param>
        /// <param name="validationFields">Список всех полей типа <see cref="string"></param>>
        /// <returns></returns>
        IQueryable GetTitleQuery(IEnumerable<string> listFields, string search, IEnumerable<string> excludeFields, IEnumerable<string> validationFields);

        /// <summary>
        /// Применить все колоночные фильтры
        /// </summary>
        /// <param name="filters">Все колоночные фильтры</param>
        /// <param name="query">Linq</param>
        /// <returns></returns>
        IQueryable GetFilterQuery(IEnumerable<object> filters);

        /// <summary>
        /// Получить сущность с полями в Select
        /// </summary>
        /// <param name="model">Модель сущности с характеристиками</param>
        /// <returns></returns>
        IQueryable GetSelect(ITableNameToModel model);

        /// <summary>
        /// Получить сущность с полями в Select для Union All
        /// </summary>
        /// <param name="model">Модель сущности с характеристиками</param>
        /// <returns></returns>
        IQueryable GetSelectUnionAll(IQueryable query, ITableNameToModel model);

        /// <summary>
        /// Получить LINQ запрос относительно DTO с фронта
        /// </summary>
        /// <param name="filters">DTO с фронта</param>
        /// <param name="listFields">Список полей</param>
        /// <returns></returns>
        IQueryable GetDataQuery(FilterQuery filters, IEnumerable<string> listFields, string dateField, ITableNameToModel model, IEnumerable<string> excludeFields);

        /// <summary>
        /// Применить паджинацию
        /// </summary>
        /// <param name="filters">DTO с фронта</param>
        /// <returns></returns>
        IQueryable GetPaginationQuery(FilterQuery filters);

        /// <summary>
        /// Получить LINQ запрос относительно DTO с фронта - результат LINQ запрос, JOIN основной и подчиненной таблицы,
        /// находящихся в отношении один-ко-многим
        /// </summary>
        /// <param name="ExtensionFilters">DTO с фронта</param>
        /// <param name="entitySubordinates">Linq подчиненной таблицы</param>
        /// <param name="query">Linq основной таблицы</param>
        /// <returns></returns>
        IQueryable GetJoinSubordinate(IQueryable query, IQueryable entitySubordinates, ExtensionFilters extensionFilters);

        /// <summary>
        /// Получить фильтр по дате для расширяющих фильтров
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        IQueryable GetDateFromToQuery(string dateFrom, string dateTo);
    }
}
