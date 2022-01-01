using MentolVKS.Common.TypeExtensions;
using MentolVKS.Data.EF.Classes.ModelsFactory;
using MentolVKS.Data.Interfaces;
using MentolVKS.Model.Bases;
using MentolVKS.Model.Filters.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;
using Microsoft.Linq.Translations;
using MentolVKS.Data.EF.Repository;

namespace MentolVKS.Data.EF.Classes
{
    /// <summary>
    /// Сформировать DLINQ запрос
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class QueryLinq<TEntity> : IQueryLinq where TEntity : EntityBase
    {

        private readonly DbSet<TEntity> _dbSet;
        public readonly DataContext _context;

        /// <summary>
        /// Общий запрос
        /// </summary>
        public IQueryable Query { get; set; }
        /// <summary>
        /// Запрос по всем полям для UNION ALL listQuery.Title
        /// </summary>
        public IQueryable TitleUnionAll { get; set; }
        /// <summary>
        /// Модель сопоставления имени таблицы с сущностью БД
        /// </summary>
        public ITableNameToModel TableNameToModel { get; set; }

        /// <summary>
        /// Словарь колоночных фильтров
        /// </summary>
        protected IDictionary<string, Func<IQueryable, FilterHandler, IQueryable>> ValueColumnFilter { get; } = new Dictionary<string, Func<IQueryable, FilterHandler, IQueryable>>();

        /// <summary>
        /// Конструктор LINQ запросов
        /// </summary>
        /// <param name="context">DataContext</param>
        /// <param name="model">Модель сопоставления имени таблицы с сущностью БД</param>
        public QueryLinq(DataContext context, ITableNameToModel model)
        {
            TableNameToModel = model;
            _context = context;
            _dbSet = _context.Set<TEntity>();
            Query = _dbSet.AsNoTracking();
            // Прочитать навигационные свойства, сформировать join
            Query = GetInclude(_context.Model.FindEntityType(typeof(TEntity)));
            // Сохранить запрос для Union ALL
            TitleUnionAll = Query;

            //Заполнить словарь описание типов колонок
            IDictionary<string, string> ListNameFieldToType = new Dictionary<string, string>();
            foreach (var properties in TableNameToModel.Model.GetType().GetProperties())
            {
                ListNameFieldToType.Add(properties.Name.ToLower(), properties.PropertyType.Name);
            }

            //Заполнить словарь колоночных фильтров
            IColumnFilters columnFilters = new ColumnFilters(ListNameFieldToType);

            ValueColumnFilter.Add(ColumnFiltersType.Select.GetDisplayName().ToLower(),
                new Func<IQueryable, FilterHandler, IQueryable>((query, item) =>
                columnFilters.GetColumnFiltersTypeSelect(query, item)));

            ValueColumnFilter.Add(ColumnFiltersType.Date.GetDisplayName().ToLower(),
                new Func<IQueryable, FilterHandler, IQueryable>((query, item) =>
                columnFilters.GetColumnFiltersTypeDate(query, item)));

            ValueColumnFilter.Add(ColumnFiltersType.Time.GetDisplayName().ToLower(),
                new Func<IQueryable, FilterHandler, IQueryable>((query, item) =>
                columnFilters.GetColumnFiltersTypeTime(query, item)));

            ValueColumnFilter.Add(ColumnFiltersType.String.GetDisplayName().ToLower(),
                new Func<IQueryable, FilterHandler, IQueryable>((query, item) =>
                columnFilters.GetColumnFiltersTypeStringTree(query, item)));

            ValueColumnFilter.Add(ColumnFiltersType.Tree.GetDisplayName().ToLower(),
                new Func<IQueryable, FilterHandler, IQueryable>((query, item) =>
                columnFilters.GetColumnFiltersTypeStringTree(query, item)));

            ValueColumnFilter.Add(ColumnFiltersType.Integer.GetDisplayName().ToLower(),
                new Func<IQueryable, FilterHandler, IQueryable>((query, item) =>
                columnFilters.GetColumnFiltersTypeInteger(query, item)));

            ValueColumnFilter.Add(ColumnFiltersType.SelectPk.GetDisplayName().ToLower(),
                new Func<IQueryable, FilterHandler, IQueryable>((query, item) =>
                columnFilters.GetColumnFiltersTypeSelectPk(query, item)));
        }


        /// <summary>
        /// Получить навигационные свойства, для построения join 
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public IQueryable GetInclude(IEntityType entityType)
        {
            // Получить свойства сущности.
            foreach (var property in entityType.GetProperties())
            {
                if (property.GetContainingForeignKeys().ToList().Count() > 0)
                {
                    IForeignKey foreignKey = property.GetContainingForeignKeys().FirstOrDefault();
                    INavigation navigation = foreignKey.DependentToPrincipal;
                    Query = ((IQueryable<TEntity>)Query).Include(navigation.Name);
                }
            };

            Query = GetSelect(TableNameToModel);

            return Query;
        }

        /// <inheritdoc/>
        public IQueryable GetSelect(ITableNameToModel model)
        {
            // Проверить Список полей перед применением фильтров
            if (model.SelectTitle != null && model.SelectTitle.Count() > 0)
            {
                string selectTitle = "";
                // Перебрать словарь и сформировать строку select
                foreach (var it in model.SelectTitle)
                {
                    selectTitle += $"{it},";
                }
                selectTitle = selectTitle.Remove(selectTitle.Length - 1);

                Query = Query.Select<TEntity>($" new ({selectTitle})");
            }
            return Query;
        }
        /// <inheritdoc/>
        public IQueryable GetSelectUnionAll(IQueryable query, ITableNameToModel model)
        {
            // Проверить Список полей перед применением фильтров
            if (model.SelectTitle != null && model.SelectTitle.Count() > 0)
            {
                string selectTitle = "";
                // Перебрать словарь и сформировать строку select
                foreach (var it in model.SelectTitle)
                {
                    selectTitle += $"{it},";
                }
                selectTitle = selectTitle.Remove(selectTitle.Length - 1);

                query = query.Select<TEntity>($" new ({selectTitle})");
            }
            return query;
        }

        /// <inheritdoc/>
        public IQueryable GetDataQuery(FilterQuery filters, IEnumerable<string> listFields, string dateField, ITableNameToModel model, IEnumerable<string> excludeFields)
        {
            // Получаем запрос на получение полного набора данных, поэтому применяем паджинацию c немедленным выполнением
            // И считаем общее количество записей
            if (string.IsNullOrEmpty(filters.TableSearchBy) && string.IsNullOrEmpty(filters.DateStart)
                && string.IsNullOrEmpty(filters.DateFinish) && filters.Filters.Count == 0 && filters.ExtensionFilters == null)
            {
                Query = Query;

            }
            else
            {
                // Формируем основной запрос с отложенным выполнением, если есть дата
                if (!string.IsNullOrEmpty(filters.DateStart) && !string.IsNullOrEmpty(filters.DateFinish))
                {
                    Query = GetDateQuery(dateField, filters);
                }
                // Добавим если есть запрос на поиск по имени по всем текстовым полям через union all
                if (!string.IsNullOrEmpty(filters.TableSearchBy))
                {
                    var stringFields = model.Model.GetType().GetProperties().Where(x => x.PropertyType == typeof(String)).Select(x => x.Name).ToList();
                    Query = GetTitleQuery(listFields, filters.TableSearchBy, excludeFields, stringFields);
                }
                // Если массив фильтров не пустой применяем фильтры
                if (filters.Filters.Any())
                {
                    Query = GetFilterQuery(filters.Filters);
                }
                if (filters.ExtensionFilters != null && filters.ExtensionFilters.Any())
                {
                    Query = GetFilterExtension(Query, filters);
                }
            }

            Query = ((IQueryable<TEntity>)Query).WithTranslations();
            return Query = Query.Distinct();
        }

        private IQueryable GetFilterExtension(IQueryable query, FilterQuery filters)
        {
            IModelFactory modelFactory = new ModelFactory();

            //Перебрать все расширения фильтров и добавить в join 
            foreach (var itemFilters in filters.ExtensionFilters)
            {

                //1. Создать запрос для подчиненной модели
                var modelSubordinate = modelFactory.TableNameToModels.FirstOrDefault(v => v.TableName == itemFilters.TableName);

                if (modelSubordinate == null)
                    throw new Exception($"Не добавлена модель {itemFilters.TableName} в файле {nameof(ModelFactory)}.cs");

                var filtersExtension = CreateListQuery(itemFilters);

                var genericMethod = GetType().GetMethod(nameof(GetQuerySubordinate), BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static).MakeGenericMethod(modelSubordinate.Model.GetType());

                // Вернуть PK из подчиненной табл, если записи есть 
                IQueryable querySubordinate = (IQueryable)genericMethod.Invoke(this, new List<object> { modelSubordinate, filtersExtension }.ToArray());

                query = GetJoinSubordinate(query, querySubordinate, itemFilters);
            }

            return query;
        }

        /// <summary>
        /// Получить linq подчиненной модели
        /// </summary>
        /// <typeparam name="K">Тип подчиненной модели</typeparam>
        /// <param name="modelSubordinate">Подчиненная модель</param>
        /// <param name="filtersExtension">dto</param>
        /// <returns></returns>
        private IQueryable GetQuerySubordinate<K>(ITableNameToModel modelSubordinate, FilterQuery filtersExtension) where K : EntityBase
        {
            IQueryLinqFactory queryLinqFactory = new QueryLinqFactory(_context);

            // Создать запрос 
            IQueryLinq queryLinq = queryLinqFactory.Create<K>(modelSubordinate);

            queryLinq.GetDataQuery(filtersExtension, null, null, modelSubordinate, modelSubordinate.ExcludeFields);

            IQueryable entitySubordinates = (IQueryable<K>)queryLinq.GetDateFromToQuery("dateFrom", "dateTo");

            return entitySubordinates;
        }

        /// <summary>
        /// Создать FilterQuery
        /// </summary>
        /// <param name="extensionFilters">Фильтр расширения</param>
        /// <returns></returns>
        private static FilterQuery CreateListQuery(ExtensionFilters extensionFilters)
        {
            //Вернуть dto
            return new FilterQuery
            {
                DateFinish = null,
                DateStart = null,
                ChkCurrentPageExport = true,
                Filters = new List<object> { new FilterHandler
                {
                    FilterType =  extensionFilters.FilterType,
                    NameField = extensionFilters.NameField,
                    TableName = extensionFilters.TableName,
                    ValuesField = (List<object>)extensionFilters.ValuesField
                } },
                Limit = 1,
                Page = 100,
                TableSearchBy = "",
                OrderBy = "asc",
                SortField = "Id",
                TableName = extensionFilters.TableName
            };
        }

        /// <inheritdoc/>
        public IQueryable GetFilterQuery(IEnumerable<object> filters)
        {
            foreach (var itemFilter in filters)
            {
                FilterHandler item = GetItemFilterHandler(itemFilter);

                // Перебрать значения фильтра и сформировать Dlinq
                var listS = item.ValuesField?.Where(x => x != null).Select(x => x.ToString()).ToList();
                if (listS != null && listS.Count > 0)
                {
                    // Так как от ag-grid-vue переодически приходят наименоваение полей с препиской _1, удалим ее из имени
                    item.NameField = item.NameField.Replace("_1", "");

                    var metodColumnFilter = ValueColumnFilter[item.FilterType.ToLower()];

                    Query = metodColumnFilter(Query, item);
                }
            }

            return Query;
        }

        /// <summary>
        /// Получить значение колоночного фильтра
        /// </summary>
        /// <param name="itemFilter"></param>
        /// <returns></returns>
        private static FilterHandler GetItemFilterHandler(object itemFilter)
        {
            if (itemFilter is FilterHandler filterHandler)
            {
                return filterHandler;
            }
            else
            {
                // Получить JObject
                JObject jObj = JObject.Parse(itemFilter.ToString());
                // Привести к типу фильтра
                return Newtonsoft.Json.JsonConvert.DeserializeObject<FilterHandler>(jObj.ToString());
            }
        }

        /// <inheritdoc/>
        public IQueryable GetDateQuery(string field, FilterQuery filterQuery)
        {
            string formatString = "yyyy-MM-dd HH:mm:ss";
            DateTime dateFinish = Convert.ToDateTime(filterQuery.DateFinish)
                .AddHours(23).AddMinutes(59).AddSeconds(59);

            return Query.Where($"{field} >= @0 AND {field} <= @1",
            DateTime.ParseExact(Convert.ToDateTime(filterQuery.DateStart).ToString(formatString), formatString, null)
            ,
            DateTime.ParseExact(dateFinish.ToString(formatString), formatString, null));
        }

        /// <inheritdoc/>
        public IQueryable GetDateFromToQuery(string dateFrom, string dateTo)
        {
            string formatString = "yyyy-MM-dd HH:mm:ss";
            DateTime dateToday = DateTime.Today;

            return Query.Where($"{dateFrom} <= @0 AND {dateTo} > @0",
                DateTime.ParseExact(dateToday.ToString(formatString), formatString, null));
        }

        /// <inheritdoc/>
        public IQueryable GetTitleQuery(IEnumerable<string> listFields, string search, IEnumerable<string> excludeFields, IEnumerable<string> validationFields)
        {
            excludeFields ??= new List<string>();
            // Получаем список полей для поиска
            listFields = listFields.Intersect(validationFields, StringComparer.CurrentCultureIgnoreCase).ToList();
            listFields = listFields.Where(x => !excludeFields.Contains(x, StringComparer.CurrentCultureIgnoreCase)).ToList();

            if (!listFields.Any())
            {
                listFields = validationFields.Except(excludeFields, StringComparer.CurrentCultureIgnoreCase).ToList();
            }

            // Инициализируем первый результат запроса
            IQueryable allResult = TitleUnionAll.Where($"{listFields.ToList()[0]}.ToLower().Contains(@0)", search.ToLower());

            // Удаляем использованное поле в запросе
            listFields.ToList().RemoveRange(0, 1);

            // Конктаенируем оставшиеся поля
            foreach (var item in listFields)
            {

                var resTemp = _dbSet.AsNoTracking();
                // Прочитать навигационные свойства, сформировать join
                GetInclude(_context.Model.FindEntityType(typeof(TEntity)));
                resTemp = (IQueryable<TEntity>)TitleUnionAll.Where($"{item}.ToLower().Contains(@0)", search);

                allResult = ((IQueryable<TEntity>)allResult).Concat(resTemp);
            }

            return allResult;
        }

        /// <inheritdoc/>
        public IQueryable GetPaginationQuery(FilterQuery filters)
        {
            // Применить сортировку до паджинации ко всему набору данных
            if (!string.IsNullOrEmpty(filters.SortField))
                Query = Query.OrderBy($"{filters.SortField} {filters.OrderBy}");

            if (filters.Page.HasValue && filters.Limit.HasValue)
            {
                Query = Query
                    .Skip((filters.Page.Value - 1) * filters.Limit.Value)
                    .Take(filters.Limit.Value);
            }

            Query = ((IQueryable<TEntity>)Query).WithTranslations();

            return Query;
        }

        /// <inheritdoc/>
        public IQueryable GetJoinSubordinate(IQueryable query, IQueryable entitySubordinates, ExtensionFilters extensionFilter)
        {
            var agGridVueRepository = new AgGridVueRepository(_context, null);
            // Получить список моделей из фабрики
            List<ITableNameToModel> models = agGridVueRepository.GetModelFactoryList().ToList();

            // Выбрать настройки join относительно основной модели
            string pk = GetPrimaryKey();

            // Найти подчиненную модель
            var searchSubordinateModel = models.FirstOrDefault(x => x.TableName.Equals(extensionFilter.TableName));

            string fk = GetForeigenKeySubordinate(searchSubordinateModel);

            query =
                ((IQueryable<TEntity>)query)
                .Join(
                    entitySubordinates,
                    $"{pk}",
                    $"{fk}",
                    $"outer");

            return query;
        }

        /// <summary>
        /// Получить первичный ключ основной модели
        /// </summary>
        /// <returns></returns>
        private string GetPrimaryKey()
        {
            string pk = "Id";

            // Выбрать настройки join относительно основной модели
            foreach (var property in _context.Model.FindEntityType(TableNameToModel.Model.GetType()).GetProperties())
            {
                if (property.FindContainingPrimaryKey() != null)
                {
                    pk = property.Name;
                }
            }

            return pk;
        }

        /// <summary>
        /// Получить вторичный ключ подчиненной таблицы
        /// </summary>
        /// <param name="modelSubordinate">Модель подчиненной таблицы</param>
        /// <returns></returns>
        private string GetForeigenKeySubordinate(ITableNameToModel modelSubordinate)
        {
            string fk = "Extid";

            foreach (var propertySubordinate in _context.Model.FindEntityType(modelSubordinate.Model.GetType()).GetProperties())
            {
                if (propertySubordinate.GetContainingForeignKeys().ToList().Any()
                    && propertySubordinate.GetContainingForeignKeys().FirstOrDefault().PrincipalEntityType.Name == _context.Model.FindEntityType(TableNameToModel.Model.GetType()).Name)
                {
                    // Это поле попало в ForeignKey
                    fk = propertySubordinate.Name;
                }
            }

            return fk;
        }
    }
}
