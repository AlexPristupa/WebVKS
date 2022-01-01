using MentolVKS.Data.Interfaces;
using MentolVKS.Model.Bases;
using MentolVKS.Model.Filters.Dto;
using MentolVKS.Model.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Linq.Translations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentolVKS.Data.EF.Classes
{
    public class ModelToQuery : IModelToQuery
    {
        /// <summary>
        /// Контекст
        /// </summary>
        private readonly DataContext _context;
        /// <summary>
        /// Главная модель
        /// </summary>
        private readonly ITableNameToModel _modelMain;

        /// <summary>
        /// DTO
        /// </summary>
        private readonly FilterQuery _listQuery;

        /// <summary>
        /// Список полей фильтрации основной таблицы
        /// </summary>
        private readonly IEnumerable<string> _listFields;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context">Контекст</param>
        /// <param name="model">Модель сопоставления имени таблицы с сущностью БД</param>
        /// <param name="listQuery"><see cref="FilterQuery"/> с фронта</param>
        /// <param name="listFields">Поля быстрого поиска по значению TableSearchBy для построения UNION ALL</param>
        public ModelToQuery(DataContext context,
            ITableNameToModel modelMain,
            FilterQuery listQuery,
            IEnumerable<string> listFields)
        {
            _context = context;
            _modelMain = modelMain; // Модель основной таблицы
            _listQuery = listQuery; // Основной таблицы
            _listFields = listFields; // Поля основной таблицы

        }
        /// <inheritdoc/>
        public async Task<dynamic> GetDataAsync<T>() where T : EntityBase
        {
            var result = new PagedCollection<T>();

            // больше одной страницы проверить page-1
            var tuple = await GetDataValuesAsync<T>();
            // больше одной страницы проверить page-1 возможно было удаление 
            if (tuple.Count / _listQuery.Limit > 1 && tuple.ResultQuery.ToList()?.Count == 0)
            {
                _listQuery.Page--;
                // сделать новый запрос
                tuple = await GetDataValuesAsync<T>();
            }

            result.Items = tuple.ResultQuery;
            result.Total = tuple.Item2;
            result.PageIndex = _listQuery.Page.HasValue ? _listQuery.Page.Value : 0;
            result.TableName = _listQuery.TableName;

            return result;
        }

        /// <summary>
        /// Получить кортеж - результирующий набор данных, количество записей в результирующем наборе данных
        /// </summary>
        /// <typeparam name="T">Модель</typeparam>
        /// <returns>Кортеж</returns>
        public async Task<(IEnumerable<T> ResultQuery, int Count)> GetDataValuesAsync<T>() where T : EntityBase
        {
            IQueryLinqFactory queryLinqFactory = new QueryLinqFactory(_context);

            // Создать запрос 
            IQueryLinq queryLinq = queryLinqFactory.Create<T>(_modelMain);

            IQueryable queryResult = queryLinq.GetDataQuery(_listQuery, _listFields, "DateRecord", _modelMain, _modelMain.ExcludeFields);

            IQueryable result = queryLinq.GetPaginationQuery(_listQuery);

            int total = await ((IQueryable<T>)queryResult).WithTranslations().CountAsync();

            List<T> resultQuery = await ((IQueryable<T>)result).ToListAsync();

            return (resultQuery, total);
        }
    }
}
