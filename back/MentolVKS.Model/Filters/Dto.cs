using MentolVKS.Common.TypeExtensions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MentolVKS.Model.Filters.Dto
{
    /// <summary>
    /// Класс фильтр vue таблицы
    /// </summary>
    public class ListQuery
    {
        /// <summary>
        /// Количество строк на странице
        /// </summary>
        public int? Limit { get; set; }
        /// <summary>
        /// Текущий номер страницы
        /// </summary>
        public int? Page { get; set; }
        /// <summary>
        /// Текстовое поле поиска
        /// </summary>
        public string TableSearchBy { get; set; }
        /// <summary>
        /// Направление сортировки
        /// </summary>
        public string OrderBy { get; set; }
        /// <summary>
        /// Название поля подлежащего сортировки
        /// </summary>
        public string SortField { get; set; }
        ///<summary>
        /// Имя таблицы
        ///</summary>
        public string TableName { get; set; }
    }

    /// <summary>
    /// Класс фильтр для журнала подписок
    /// </summary>
    public class SubscriptionJournalQuery : ListQuery
    {
        /// <summary>
        /// Дата начала выбора данных 
        /// </summary>       
        public string DateStart { get; set; }
        /// <summary>
        /// Дата окончания выбора данных 
        /// </summary>
        public string DateFinish { get; set; }

        /// <summary>
        /// Признак экспорта в файл текущей страницы
        /// </summary>
        public bool ChkCurrentPageExport { get; set; } = false;
        /// <summary>
        /// Список фильтров
        /// </summary>
        public List<object> Filters { get; set; }
    }

    /// <summary>
    /// Класс фильтр для AgGridVue
    /// </summary>
    /// 
    public class FilterQuery : ListQuery
    {
        /// <summary>
        /// Дата начала выбора данных 
        /// </summary>       
        public string DateStart { get; set; }
        /// <summary>
        /// Дата окончания выбора данных 
        /// </summary>
        public string DateFinish { get; set; }

        /// <summary>
        /// Признак экспорта в файл текущей страницы
        /// </summary>
        public bool ChkCurrentPageExport { get; set; } = false;
        /// <summary>
        /// Список фильтров
        /// </summary>
        public List<object> Filters { get; set; }

        /// <summary>
        /// Фильтр расширения для таблицы переданной в DTO FilterQuery.TableName
        /// </summary>
        public IEnumerable<ExtensionFilters> ExtensionFilters { get; set; }
        /// Создать listQuery для использование на бэке
        /// </summary>
        /// <param name="tableName">Имя таблицы</param>
        /// <param name="nameField">Имя поля фильтрации</param>
        /// <param name="valuesField">Значение фильтрации</param>
        /// <param name="filterType">Тип фильтрации</param>
        /// <param name="tableSearchBy">Значение быстрого поиска</param>
        /// <param name="orderBy">Направление сортировки</param>
        /// <param name="dateStart">Начальная дата фильтрации</param>
        /// <param name="dateFinish">Конечная дата фильтрации</param>
        /// <param name="chkCurrentPageExport">Признак выгрузки текущей страницы в файл</param>
        /// <param name="limit">Количество строк на странице</param>
        /// <param name="page">Номер страницы пагинации</param>
        /// <param name="sortField">Имя поля сортировки</param>
        /// <returns></returns>
        public static FilterQuery Create(string tableName, string nameField, object valuesField, string filterType,
            string tableSearchBy = "", string orderBy = "asc",
            string dateStart = "", string dateFinish = "", bool chkCurrentPageExport = false,
            int? limit = null, int? page = 1, string sortField = "Id")
        {
            //Сформировать массив значений фильтра
            List<object> valuesFieldList;
            if (valuesField is List<object> v)
                valuesFieldList = v;
            else
                valuesFieldList = new List<object> { valuesField };

            //Вернуть dto
            return new FilterQuery
            {
                DateFinish = dateFinish,
                DateStart = dateStart,
                ChkCurrentPageExport = chkCurrentPageExport,
                Filters = new List<object> { new FilterHandler
                {
                    FilterType = filterType,
                    NameField = nameField,
                    TableName = tableName,
                    ValuesField = valuesFieldList
                } },
                Limit = limit,
                Page = page,
                TableSearchBy = tableSearchBy,
                OrderBy = orderBy,
                SortField = sortField,
                TableName = tableName
            };
        }

        /// <summary>
        /// Парсинг DTO фронта в DTO для бэка
        /// </summary>
        /// <param name="listQuery">DTO фронта</param>
        /// <returns></returns>
        public virtual FilterParametrsDto GetFilterParametrsDto(FilterQuery listQuery)
        {
            var filter = new FilterParametrsDto
            {
                Start = listQuery.Page.Value,
                Search = listQuery.TableSearchBy,
                Length = listQuery.Limit.Value,
                OrderColumnName = string.IsNullOrEmpty(listQuery.SortField) ? "Id" : listQuery.SortField,
                TableName = listQuery.TableName,
                ColumnFilters = GetColumnFilters(listQuery.Filters),
                IsApply = true,
                OrderDir = (SortDirection)(listQuery.OrderBy == "ascending" ? SortDirection.Ascending : SortDirection.Descending)
            };

            return filter;
        }
        /// <summary>
        /// Распарсить массив колоночных фильтров
        /// </summary>
        /// <param name="filters">массив фильтров</param>
        /// <returns></returns>

        private List<ColumnFiltersDto> GetColumnFilters(List<object> filters)
        {
            List<ColumnFiltersDto> columnFiltersDtos = new List<ColumnFiltersDto>();

            foreach (var itF in filters)
            {
                // Получить JObject
                JObject jObj = JObject.Parse(itF.ToString());
                // Привести к типу фильтра
                FilterHandler item = Newtonsoft.Json.JsonConvert.DeserializeObject<FilterHandler>(jObj.ToString());
                // Перебрать значения фильтра и сформировать Dlinq
                List<string> listS = item.ValuesField.Select(x => x.ToString()).ToList();
                if (listS.Count > 0)
                {
                    // Так как от ag-grid-vue переодически приходят наименоваение полей с препиской _1, удалим ее из имени
                    item.NameField = item.NameField.Replace("_1", "");

                    if (item.FilterType.Is(ColumnFiltersType.Select.GetDisplayName().ToLower())) // "select"
                    {
                        List<string> selectList = new List<string>();
                        foreach (var it in listS)
                        {
                            selectList.Add(it);
                        }
                        ColumnFiltersDto columnFiltersDto = new ColumnFiltersDto
                        {
                            Type = ColumnFiltersType.Select,
                            ColumnName = item.NameField,
                            SelectList = selectList.ToArray()
                        };
                        columnFiltersDtos.Add(columnFiltersDto);
                    }
                    else if (item.FilterType.Is(ColumnFiltersType.Date.GetDisplayName().ToLower())) // "date"
                    {
                        ColumnFiltersDto columnFiltersDto = new ColumnFiltersDto
                        {
                            Type = ColumnFiltersType.Date,
                            ColumnName = item.NameField,
                            OperandDate = new string[2],
                            SelectDate = new string[2],
                            SelectTime = new string[2]
                        };


                        DateTime dF = DateTime.Today;
                        DateTime dS = DateTime.Today;
                        int i = 0;
                        foreach (var v in item.ValuesField)
                        {
                            JObject ObjectFiltersDate = v as JObject;
                            var children = ObjectFiltersDate.Children();
                            // Названия очередности операции, как они названы на фронте
                            List<string> priznakOperations = new List<string> { "selectFirstContains", "selectSecondContains" };
                            // Значение даты операции, как они названы на фронте
                            List<string> dateOperations = new List<string> { "valueFirstDateFilter", "valueSecondDateFilter" };
                            // Значение времени операции, как они названы на фронте
                            List<string> timeOperations = new List<string> { "timerFirstFilter", "timeSecondFilter" };
                            // Значение даты для подстановки в запрос

                            // TODO: Проткстить формирование DLINQ
                            string dateS = "";

                            foreach (var itemChildren in children)
                            {
                                JProperty poperty = itemChildren as JProperty;


                                // Выбираем знак сравнения для операции
                                if (priznakOperations.Contains(poperty.Name))
                                {
                                    columnFiltersDto.OperandDate[i] = poperty.Value.ToString();
                                    //valueDateFilter += $"{item.NameField} {poperty.Value} ";
                                }
                                // Подставляем значение даты
                                else if (dateOperations.Contains(poperty.Name))
                                {
                                    // dateS = poperty.Value.ToString();
                                    columnFiltersDto.SelectDate[i] = poperty.Value.ToString();
                                }
                                // Подставляем значение времени
                                else if (timeOperations.Contains(poperty.Name))
                                {
                                    // dateS += " " + poperty.Value;
                                    if (poperty.Name == "timerFirstFilter")
                                    {
                                        // dF = Convert.ToDateTime(dateS);
                                        columnFiltersDto.SelectTime[0] = poperty.Value.ToString(); //dF.ToString("yyyy-MM-dd HH:mm:ss");
                                    }
                                    else
                                    {
                                        // dS = Convert.ToDateTime(dateS);
                                        columnFiltersDto.SelectTime[1] = poperty.Value.ToString(); //  dS.ToString("yyyy-MM-dd HH:mm:ss");
                                    }
                                }
                            }
                            i++;
                        }
                        columnFiltersDtos.Add(columnFiltersDto);
                    }
                    else if (item.FilterType.Is(ColumnFiltersType.Integer.GetDisplayName().ToLower())) // "integer"
                    {
                        ColumnFiltersDto columnFiltersDto = new ColumnFiltersDto
                        {
                            Type = ColumnFiltersType.Integer,
                            ColumnName = item.NameField
                        };

                        foreach (var v in item.ValuesField)
                        {
                            (string, string) valueFilter = ParseValueFilter(v);
                            columnFiltersDto.Operand = valueFilter.Item1.Trim();
                            columnFiltersDto.StrSearch = valueFilter.Item2.Trim();
                            columnFiltersDtos.Add(columnFiltersDto);
                        }
                    }
                    else if (item.FilterType.Is(ColumnFiltersType.Tree.GetDisplayName().ToLower()) || item.FilterType.Is(ColumnFiltersType.String.GetDisplayName().ToLower())) // "tree" "string"
                    {
                        ColumnFiltersDto columnFiltersDto = new ColumnFiltersDto
                        {
                            Type = ColumnFiltersType.Tree,
                            ColumnName = item.NameField
                        };

                        List<StringFilterParametrsDto> stringFilterParametrsDtos = new List<StringFilterParametrsDto>();
                        foreach (var v in item.ValuesField)
                        {
                            (string, string) valueFilter = ParseValueFilter(v);

                            StringFilterParametrsDto stringFilterParametrsDto = new StringFilterParametrsDto()
                            {
                                Operand = valueFilter.Item1.Trim(),
                                StrSearch = valueFilter.Item2.Trim()
                            };
                            stringFilterParametrsDtos.Add(stringFilterParametrsDto);
                        }
                        columnFiltersDto.StringSearch = stringFilterParametrsDtos.ToArray();
                        columnFiltersDtos.Add(columnFiltersDto);
                    }
                }
            }

            return columnFiltersDtos;
        }

        /// <summary>
        /// Парсинг значений фильтра
        /// </summary>
        /// <param name="v">Объект фильтра</param>
        /// <returns></returns>
        private (string, string) ParseValueFilter(object v)
        {
            JObject ObjectFiltersDate = v as JObject;
            var children = ObjectFiltersDate.Children();

            // Названия очередности операции, как они названы на фронте
            List<string> priznakOperations = new List<string> { "operand" };
            // Значение операции, как они названы на фронте
            List<string> stringOperations = new List<string> { "compareValue" };

            // Значение даты для подстановки в запрос
            string operand = "";
            string stringValue = "";

            foreach (var itemChildren in children)
            {
                JProperty poperty = itemChildren as JProperty;
                // Выбираем знак сравнения для операции
                if (priznakOperations.Contains(poperty.Name))
                {
                    operand = $"{poperty.Value}";
                }
                // Подставляем значение фильтра
                else if (stringOperations.Contains(poperty.Name))
                {
                    stringValue = poperty.Value.ToString();
                }
            }

            return (operand, stringValue);
        }

        /// <summary>
        /// Валидация dto для фабрики GET запросов.
        /// </summary>
        /// <param name="listQuery">DTO</param>
        /// <returns>Результат валидации: True или False с сообщением об ошибке.</returns>
        public static FilterQueryValidateResult Validate(FilterQuery listQuery)
        {
            if (listQuery == null)
                return new FilterQueryValidateResult { IsValid = false, ErrorMessage = $"Ошибка преобразования тела запроса в {nameof(FilterQuery)}" };

            if (string.IsNullOrWhiteSpace(listQuery.SortField) && !string.IsNullOrWhiteSpace(listQuery.OrderBy))
                return new FilterQueryValidateResult { IsValid = false, ErrorMessage = $"Для сортировки заполните пустое поле {nameof(listQuery.SortField)}" };

            if (listQuery.Filters == null)
                return new FilterQueryValidateResult { IsValid = false, ErrorMessage = $"Для фильтрации {nameof(listQuery.Filters)} [] или массив значений" };

            for (var i = 0; i < listQuery.Filters.Count; i++)
            {
                var filterBody = listQuery.Filters[i].ToString();
                if (!filterBody.Contains(nameof(FilterHandler.FilterType), StringComparison.InvariantCultureIgnoreCase)
                    || !filterBody.Contains(nameof(FilterHandler.NameField), StringComparison.InvariantCultureIgnoreCase)
                    || !filterBody.Contains(nameof(FilterHandler.TableName), StringComparison.InvariantCultureIgnoreCase)
                    || !filterBody.Contains(nameof(FilterHandler.ValuesField), StringComparison.InvariantCultureIgnoreCase))
                    return new FilterQueryValidateResult
                    {
                        IsValid = false,
                        ErrorMessage = "Одно из имён полей фильтра неправильное. " +
                                      $"Правильные имена полей: {nameof(FilterHandler.FilterType)}, {nameof(FilterHandler.NameField)}, " +
                                      $"{nameof(FilterHandler.TableName)}, {nameof(FilterHandler.ValuesField)}"
                    };
            }

            return new FilterQueryValidateResult { IsValid = true };
        }
    }

    /// <summary>
    /// Фильтр задаваемый в шапке таблицы
    /// </summary>
    public class FilterHandler
    {
        /// <summary>
        /// Имя таблицы, из которой поступили данные для фильтрации
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// Имя поля данных для фильтрации
        /// </summary>
        public string NameField { get; set; }

        /// <summary>
        /// Тип фильтра (date, select)
        /// </summary>
        public string FilterType { get; set; }
        /// <summary>
        /// Массив значений фильтруемого поля
        /// </summary>
        public List<object> ValuesField { get; set; }
    }
}
