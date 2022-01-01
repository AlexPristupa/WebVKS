using MentolVKS.Common;
using MentolVKS.Common.TypeExtensions;
using MentolVKS.Model.BaseModel;
using MentolVKS.Model.Bases;
using MentolVKS.Model.Filters;
using MentolVKS.Model.Filters.Dto;
using MentolVKS.Model.Filters.Enum;
using MentolVKS.Model.ViewModel;
using MentolVKS.Service.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentolVKS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SelectFilterController : ApiControllerBase
    {
        IStringLocalizer<SelectFilterController> _localizer;
        private int _limitRespone = 300;
        public SelectFilterController(IService service, IStringLocalizer<SelectFilterController> localizer) : base(service)
        {
            _localizer = localizer;
        }
        /// <summary>
        /// Фильтр с выборкой
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     POST /api/SelectFilter
        ///     {
        ///         FilterId: -1,                           Тип фильтра
        ///         ColumnName: 'DateRecord'                Имя колонки для фильтрации
        ///         TableName: 'tar_notify_log',   Имя таблицы
        ///         Search: 'что-то ищем',                  Строка поиска
        ///         Limit: 'Ограничение по выборке 0 - без ограничения',        Строка поиска
        ///         CheckList: ['да', 'нет']                Список выбранных значений в колоночном фильтре
        ///         AddParameters: []                       Дополнительные параметры в vue компонентах не используется, используется в jquery
        ///         
        /// Пример ответа:
        /// 
        ///     AjaxOperationResult
        ///     {
        ///         result: 0,                              0 - Success, -1 - Error, -2 - Warning
        ///         message: 'Сообщение об ошибке или варнинге',
        ///         data: {Набор данных возвращаемый из базы данных}
        ///     }
        /// </remarks>
        /// <param name="queryFilter"></param>
        /// <returns>Возвращаем отфильтрованный, отсортированный набор данных с применением пагинации в типе AjaxOperationResult для фильтра типа select</returns>
        /// <response code="200">Возвращает созданный AjaxOperationResult</response>
        [HttpPost]
        [RequestFormSizeLimit(4000)]
        public async Task<AjaxOperationResult> Post([FromBody] QueryFilter queryFilter)
        {
            //Ag-grid-vue, в какой-то момент начинает дописывать к именам полей "_1"
            string columnName = queryFilter.ColumnName.Replace("_1", string.Empty);
            var ListNamePostReUse = new List<string>();
            try
            {
                FilterParametrsDto filter = new FilterParametrsDto() { Limit = queryFilter.Limit.Value, Search = queryFilter.Search };

                object[] addParameters = queryFilter.AddParameters.ToArray();

                if (addParameters.Count() == 0) addParameters = new string[0];

                var selectFilter = new ColumnForSelectFilter();

                var user = await Service.GetUserByLoginAsync(User.Identity.Name); 
                
                var tuple = await Service.ColumnForStringFilterGetValueMemberAsync(queryFilter.FilterId, columnName, queryFilter.TableName, user.Id, addParameters, filter);

                var rows = tuple.Item2.ToList();

                rows = rows.Where(v => v.Value != null).ToList();

                //Отберем значения только для выбранных structureId
                if (queryFilter.StatusStructureForSelect.Is(StatusStructureForSelect.YesStructure.GetDisplayName()))
                {
                    rows = GetRowsFilter(rows, (IEnumerable<string>)queryFilter.CheckList).ToList();
                }
                else if (queryFilter.StatusStructureForSelect.Is(StatusStructureForSelect.ReuseStructure.GetDisplayName()))
                {
                    rows = GetRowsFilter(rows, ListNamePostReUse).ToList();
                }

                var check = rows.Where(i => i.Value != null).Where(i => queryFilter.CheckList.Contains(i.Value.Trim())).ToList();

                rows = queryFilter.Limit == 0 ? rows.Distinct().Take(_limitRespone).ToList() : rows.Distinct().ToList();

                if (check.Count == rows.Count) selectFilter.CheckAll = "check";

                selectFilter.ColumnForStringFilter = check.Union(rows.OrderBy(t => t.Value));

                foreach (var x in selectFilter.ColumnForStringFilter)
                {
                    x.State = "check";
                }

                selectFilter.ValueMember = string.IsNullOrEmpty(tuple.Item1) || tuple.Item1 == ValueMemberType.Value.GetDisplayName() ? ValueMemberType.Value.GetDisplayName() : ValueMemberType.Id.GetDisplayName();

                return AjaxOperationResult.Success(selectFilter);
            }
            catch (Exception err)
            {
                return AjaxOperationResult.Error(err.Message);
            }
        }

        /// <summary>
        /// Получить списки переиспользуемые в фильтре выбора
        /// </summary>
        /// <param name="checksList"></param>
        /// <returns></returns>
        private async Task<(IEnumerable<string>, IEnumerable<string>)> GetListPostForStructureReuseAsync(IEnumerable<object> checksList)
        {
            // список приведения structureId к наименованию должностей
            List<string> listNamePost = new List<string>();

            // список выбранных должностей в колоночном фильтре
            List<string> listCheckNamesPost = new List<string>();

            // список SelectStructuresForPostDto из фильтра структура
            List<object> listStructuresForPostDto = new List<object>();
            foreach (var item in checksList)
            {
                try
                {
                    ValuesFieldDto selectStructuresForPostDto = JsonConvert.DeserializeObject<ValuesFieldDto>(item.ToString());
                    listStructuresForPostDto.Add(item);
                }
                catch
                {
                    listCheckNamesPost.Add((string)item);
                }
            }

            return (listNamePost, listCheckNamesPost);
        }

        /// <summary>
        /// Получить список относительно structureId
        /// </summary>
        /// <param name="rows">Полный список</param>
        /// <param name="checksList">Список выбора</param>
        /// <returns>Отфильтрованный список</returns>
        private IEnumerable<ColumnForStringFilter> GetRowsFilter(IEnumerable<ColumnForStringFilter> rows, IEnumerable<string> checksList) => rows.Where(x => checksList.Contains(x.Value)).ToList();

        /// <summary>
        /// Получает список значений для фильтра
        /// </summary>
        /// <param name="tableName">Наименование таблицы</param>
        [HttpGet(nameof(GetSelectListFilterName))]
        public async Task<AjaxOperationResult> GetSelectListFilterName(string tableName)
        {
            try
            {
                return AjaxOperationResult.Success(await GetSelectListFilterNames(tableName, User.Identity.Name));
            }
            catch (Exception err)
            {
                return AjaxOperationResult.Error(err.Message);
            }
        }

        /// <summary>
        /// Получает список значений для фильтра
        /// </summary>
        /// <param name="tableName">Наименование таблицы</param>
        /// <param name="userName">Имя пользователя</param>
        /// <param name="isCommon">Общий фильтр</param>
        private async Task<FilterListViewModel> GetSelectListFilterNames(string tableName, string userName, int? isCommon = null)
        {
            var userId = (await Service.GetUserByLoginAsync(userName))?.Id ?? 1;

            //Активный фильтр пользователя всегда будет первым!
            var filters = await Service.FilterNameAllByTableAndUserIdAsync(tableName, userId);

            if (isCommon.HasValue)
            {
                filters = filters.Where(f => f.IsCommon == isCommon);
            }

            var filterNamesList = filters.OrderByDescending(c => c.IsActive).ToList();

            var types = filterNamesList.Select(f => new SelectListItem
            {
                Text = f.Name,
                Value = f.Id.ToString()
            }).ToList();

            types.Add(new SelectListItem
            {
                Text = "<нет фильтра>",
                Value = "-1"
            });

            var model = new FilterListViewModel
            {
                SelectFilterList = types,
                TableName = tableName
            };

            return model;
        }

        /// <summary>
        /// Сохраняет фильтры из комбика
        /// </summary>
        /// <param name="saveFiltersDto">Dto сохраняемых фильтров</param>       
        [HttpPost("saveFilter")]
        public async Task<AjaxOperationResult> SaveFilter([FromBody] SaveFiltersDto saveFiltersDto)
        {
            try
            {
                FilterParametrsDto filter = saveFiltersDto.ListQuery.GetFilterParametrsDto(saveFiltersDto.ListQuery);
                var table = await Service.FilterTablesListGetByTableAsync(filter.TableName);
                var userId = 1;
                var user = await GetCurrentUserAsync();

                if (user != null) userId = user.Id;

                if (saveFiltersDto.Func.Is("save"))
                {
                    var newName = string.IsNullOrEmpty(saveFiltersDto.NewName) ? "<Новый фильтр>" : saveFiltersDto.NewName;

                    var filters = await GetSelectListFilterNames(filter.TableName, User.Identity.Name, saveFiltersDto.Iscommon);

                    if (filters.SelectFilterList.Any(f => !f.Value.Is("-1") && f.Text.Is(newName)))
                    {
                        return AjaxOperationResult.Warning("Фильтр с таким именем уже присутствует в системе");
                    }


                    var newFilterList = new FiltersList
                    {
                        FilterName = newName,
                        IsCommon = saveFiltersDto.Iscommon
                    };


                    await Service.FiltersListAddAsync(newFilterList);

                    if (filter.ColumnFilters.Any())
                    {
                        foreach (var f in filter.ColumnFilters)
                        {
                            var colList = await Service.FilterColumnsListGetByColumnAndTableAsync(f.ColumnName, table.TableName);

                            if (colList.Any()) await AddNewFilterValuesAsync(f, newFilterList.Id, colList.First());
                        }
                    }

                    var ful = new FiltersToUserLink
                    {
                        FilterId = newFilterList.Id,
                        UserId = userId,
                        IsActive = 0
                    };

                    await Service.FiltersToUserLinkAddAsync(ful);

                    return AjaxOperationResult.Success(newFilterList.Id);
                }

                var row = (await Service.FilterColumnsListGetByFilterAndTableAsync(filter.FilterId, table.TableName)).ToList();

                foreach (var r in row)
                {
                    var v = await Service.FilterValueAllByFilterAndColumnAsync(filter.FilterId, r.Id);

                    if (v.Any())
                    {
                        foreach (var tv in v)
                        {
                            await Service.FilterValueRemoveAsync(tv);
                        }
                    }
                }

                if (filter.ColumnFilters != null)
                {
                    foreach (var f in filter.ColumnFilters)
                    {
                        var newFilterColumnList = (await Service.FilterColumnsListGetByColumnAndTableAsync(f.ColumnName, table.TableName))
                            .FirstOrDefault();

                        if (newFilterColumnList != null)
                        {
                            var fv = (await Service.FilterValueAllByFilterAndColumnAsync(filter.FilterId,
                                newFilterColumnList.Id)).ToList();

                            if (fv.Any())
                            {
                                foreach (var v in fv)
                                {
                                    await Service.FilterValueRemoveAsync(v);
                                }
                            }

                            await AddNewFilterValuesAsync(f, filter.FilterId, newFilterColumnList);
                        }
                    }
                }

                if ((await GetSelectListFilterNames(filter.TableName, User.Identity.Name)).SelectFilterList.Count() == 1)
                {
                    var flist = await Service.FiltersListGetByIdAsync(filter.FilterId);

                    if (flist != null)
                    {
                        await Service.FiltersListRemoveAsync(flist);
                    }

                    return AjaxOperationResult.Success(-1);
                }

                return AjaxOperationResult.Success(filter.FilterId);
            }
            catch (Exception err)
            {
                return AjaxOperationResult.Error(err.Message);
            }
        }

        /// <summary>
        /// Добавляет значение фильтра
        /// </summary>
        /// <param name="f">Данные фильтра</param>
        /// <param name="filterId">Идентификатор фильтра</param>
        /// <param name="fcl">Список фильтров для колонки</param>
        private async Task AddNewFilterValuesAsync(ColumnFiltersDto f, int filterId, FilterColumnsList fcl)
        {
            if (f.Type == ColumnFiltersType.Select && f.SelectList != null)
            {
                foreach (var st in f.SelectList)
                {
                    var newFilterValue = new FilterValue
                    {
                        FilterId = filterId,
                        ColumnId = fcl.Id,
                        Fvalue = st
                    };

                    await Service.FilterValueAddAsync(newFilterValue);
                }
            }

            if (f.Type == ColumnFiltersType.String || f.Type == ColumnFiltersType.Tree)
            {
                foreach (var buf in f.StringSearch)
                {
                    var newFilterOperationList = await Service.FilterOperationsListGetByOperandAsync(buf.Operand);
                    var newFilterValue = new FilterValue
                    {
                        FilterId = filterId,
                        ColumnId = fcl.Id,
                        Fvalue = buf.StrSearch,
                        OperationId = newFilterOperationList.Id
                    };

                    await Service.FilterValueAddAsync(newFilterValue);
                }
            }

            if (!string.IsNullOrEmpty(f.Operand) && f.Type == ColumnFiltersType.Integer && !string.IsNullOrEmpty(f.StrSearch))
            {
                var newFilterOperationList = await Service.FilterOperationsListGetByOperandAsync(f.Operand);

                if (newFilterOperationList != null)
                {
                    var newFilterValue = new FilterValue
                    {
                        FilterId = filterId,
                        ColumnId = fcl.Id,
                        Fvalue = f.StrSearch,
                        OperationId = newFilterOperationList.Id
                    };
                    await Service.FilterValueAddAsync(newFilterValue);
                }
            }

            if (f.ColumnName.Is("MOSstatus") && f.Type == ColumnFiltersType.Integer)
            {
                var equalFilterOperation = await Service.FilterOperationsListGetByOperandAsync("=");

                if (equalFilterOperation != null)
                {
                    foreach (var st in f.SelectList)
                    {
                        var newFilterValue = new FilterValue
                        {
                            FilterId = filterId,
                            ColumnId = fcl.Id,
                            Fvalue = st,
                            OperationId = equalFilterOperation.Id
                        };

                        await Service.FilterValueAddAsync(newFilterValue);
                    }
                }
            }

            if (f.Type == ColumnFiltersType.Date && f.SelectDate.Any() && f.OperandDate.Any())
            {
                for (var i = 0; i < f.SelectDate.Length; i++)
                {
                    var newFilterOperationList = await Service.FilterOperationsListGetByOperandAsync(f.OperandDate[i]);

                    if (newFilterOperationList != null)
                    {
                        var newFilterValue = new FilterValue
                        {
                            FilterId = filterId,
                            ColumnId = fcl.Id,
                            Fvalue = f.SelectDate[i]+" "+f.SelectTime[i],
                            OperationId = newFilterOperationList.Id
                        };

                        await Service.FilterValueAddAsync(newFilterValue);
                    }
                }
            }

            if (f.Type == ColumnFiltersType.Time && f.SelectTime.Any() && f.OperandTime.Any())
            {
                for (var i = 0; i < f.SelectTime.Length; i++)
                {
                    var newFilterOperationList = await Service.FilterOperationsListGetByOperandAsync(f.OperandTime[i]);

                    if (newFilterOperationList != null)
                    {
                        var newFilterValue = new FilterValue
                        {
                            FilterId = filterId,
                            ColumnId = fcl.Id
                        };

                        if (!string.IsNullOrEmpty(f.SelectTime[i]) && f.SelectTime[i].IndexOf('_') != -1)
                            f.SelectTime[i] = f.SelectTime[i].Replace('_', '0');

                        newFilterValue.Fvalue = f.SelectTime[i];
                        newFilterValue.OperationId = newFilterOperationList.Id;

                        await Service.FilterValueAddAsync(newFilterValue);
                    }
                }
            }
        }

        /// <summary>
        /// Получает имя колонки из фильтра
        /// </summary>
        /// <param name="filterId">Идентификатор фильтра</param>
        [HttpGet("getColumNameByFilter")]
        public async Task<AjaxOperationResult> GetColumNameByFilter(int filterId)
        {
            try
            {
                var filterColumns = await Service.FilterColumnAllByFilterIdAsync(filterId);

                return AjaxOperationResult.Success(!filterColumns.Any() ? Json(false) : Json(filterColumns.Select(x => new
                {
                    ColumnName = x.ColumnName.ToCamelCase(),
                    x.Id,
                    x.TypeId,
                    x.TypeName
                })));
            }
            catch (Exception err)
            {
                return AjaxOperationResult.Error(err.Message);
            }
        }

        /// <summary>
        /// Получает тип фильтра
        /// </summary>
        /// <param name="typeFilter">dto фильтра</param>
        [HttpPost("getTypeFilter")]
        public async Task<AjaxOperationResult> GetTypeFilter([FromBody] TypeSelectFilter typeFilter)
        {
            try
            {
                var rows = await Service.FilterForColumnTypeListGetTypeFilterAsync(typeFilter.FilterId, typeFilter.ColumnName, typeFilter.TableName);

                return rows.Any()
                    ? AjaxOperationResult.Success(((ColumnFiltersType)rows.First().TypeId).ToString().ToCamelCase())
                    : AjaxOperationResult.Success(false);
            }
            catch (Exception err)
            {
                return AjaxOperationResult.Error(err.Message);
            }
        }
        /// <summary>
        /// Получение активного строкового фильтра пользователя
        /// </summary>
        /// <param name="typeFilter">dto фильтра</param>
        [HttpPost("getActiveStringFilter")]
        public async Task<AjaxOperationResult> GetActiveStringFilter([FromBody] TypeSelectFilter typeFilter)
        {
            try
            {
                /*string[] addParameters = null;
                FilterParametrsDto columnFilters = new FilterParametrsDto();
                if (addParameters == null) addParameters = new string[0];

                var user = await GetCurrentUserAsync();
                var rows = (await Service.ColumnForStringFilterGetValueAsync(typeFilter.FilterId, typeFilter.ColumnName, typeFilter.TableName, user.Id, addParameters, columnFilters)).Where(t => t.State.Is("check"));
                var stateList = new string[rows.Count()];
                var i = 0;

                foreach (var t in rows)
                {
                    stateList[i] = t.Value;
                    i++;
                }

                return AjaxOperationResult.Success(stateList);*/
                var rows = await Service.ColumnForIntegerFilterGetByFilterAsync(typeFilter.FilterId, typeFilter.ColumnName, typeFilter.TableName);
                if (!rows.Any())
                {
                    return AjaxOperationResult.Success(false);
                }
                return AjaxOperationResult.Success(rows.Select(c => new { Operand = c.Operand, CompareValue = c.Value }).ToList());
                
            }
            catch (Exception err)
            {
                return AjaxOperationResult.Error(err.Message);
            }
        }

        /// <summary>
        /// Получение активного числового фильтра пользователя
        /// </summary>
        /// <param name="typeFilter">dto фильтра</param>
        /// <returns></returns>
        [HttpPost("getActiveIntegerFilter")]
        public async Task<AjaxOperationResult> GetActiveIntegerFilter([FromBody] TypeSelectFilter typeFilter)
        {
            try
            {
                var rows = await Service.ColumnForIntegerFilterGetByFilterAsync(typeFilter.FilterId, typeFilter.ColumnName, typeFilter.TableName);
                return AjaxOperationResult.Success(!rows.Any() ? JsonConvert.SerializeObject(false) : JsonConvert.SerializeObject(rows));
            }
            catch (Exception err)
            {
                return AjaxOperationResult.Error(err.Message);
            }
        }

        /// <summary>
        /// Получает фильтр даты
        /// </summary>
        /// <param name="typeFilter">dto фильтра</param>
        [HttpPost("getActiveDateFilter")]
        public async Task<AjaxOperationResult> GetActiveDateFilter([FromBody] TypeSelectFilter typeFilter)
        {
            try
            {
                var rows = (await Service.ColumnForIntegerFilterGetByFilterAsync(typeFilter.FilterId, typeFilter.ColumnName, typeFilter.TableName)).ToList();

                if (!rows.Any() && rows.Count<2)
                {
                    return AjaxOperationResult.Success(false);
                }

                var firstDate = DateTime.Parse(rows[0].Value);
                var lastDate = DateTime.Parse(rows[1].Value);

                var first = new { selectFirstContains = rows[0].Operand, valueFirstDateFilter = firstDate.ToString("dd.MM.yyyy"), timerFirstFilter= firstDate.ToString("HH:mm:ss") };
                var last = new { selectSecondContains = rows[1].Operand, valueSecondDateFilter = lastDate.ToString("dd.MM.yyyy"), timeSecondFilter= lastDate.ToString("HH:mm:ss") };
                
                var r = new List<dynamic>();
                r.Add(first);
                r.Add(last);

                return AjaxOperationResult.Success(r);
            }
            catch (Exception err)
            {
                return AjaxOperationResult.Error(err.Message);
            }
        }

        /// <summary>
        /// Получает фильтр времени
        /// </summary>
        /// <param name="typeFilter">dto фильтра</param>
        [HttpPost("getActiveTimeFilter")]
        public async Task<AjaxOperationResult> GetActiveTimeFilter([FromBody] TypeSelectFilter typeFilter)
        {
            try
            {
                var rows = (await Service.ColumnForIntegerFilterGetByFilterAsync(typeFilter.FilterId, typeFilter.ColumnName, typeFilter.TableName)).ToList();
                var r = new ColumnForDateFilter
                {
                    Operand = new string[rows.Count],
                    Value = new string[rows.Count],
                    IsEmptyDate = new int[rows.Count]
                };

                for (var i = 0; i < rows.Count; i++)
                {
                    r.Operand[i] = rows[i].Operand;
                    r.Value[i] = rows[i].Value;
                    r.IsEmptyDate[i] = rows[i].IsEmptyDate;
                }

                return AjaxOperationResult.Success(!rows.Any() ? JsonConvert.SerializeObject(false) : JsonConvert.SerializeObject(r));
            }
            catch (Exception err)
            {
                return AjaxOperationResult.Error(err.Message);
            }
        }

        /// <summary>
        /// Меняет имя фильтра
        /// </summary>
        /// <param name="renameFilter">dto изменения имени фильтра</param>

        [HttpPost("renameFilter")]
        public async Task<AjaxOperationResult> RenameFilter([FromBody] RenameFilter renameFilter)
        {
            try
            {
                var row = await Service.FiltersListGetByIdAsync(renameFilter.FilterId);
                if (row == null) return AjaxOperationResult.Success(new { success = false, responseText = "Не найдена запись в базе данных для изменения." });

                if (!await Service.FiltersListCheckNameUniqueAsync(renameFilter.NewNameFilter, renameFilter.FilterId))
                {
                    return AjaxOperationResult.Warning(_localizer["Filter with this name is already present in the system"].Value);
                }

                row.FilterName = renameFilter.NewNameFilter;
                await Service.FiltersListSaveAsync(row);

                return AjaxOperationResult.Success(new { success = true, responseText = "Запись успешно изменена." });
            }
            catch (Exception err)
            {
                return AjaxOperationResult.Error(err.Message);
            }
        }
        /// <summary>
        /// Удаляет фильтра
        /// </summary>
        /// <param name="filterId">Идентификатор фильтра</param>
        [HttpDelete("deleteFilter")]
        public async Task<AjaxOperationResult> DeleteFilter(int id)
        {
            try
            {
                var row = await Service.FiltersListGetByIdAsync(id);
                if (row == null) return AjaxOperationResult.Success(new { success = false, responseText = "Не найдена запись в базе данных для удаления." });

                var filVal = await Service.FilterValueAllByFilterIdAsync(id);

                foreach (var fv in filVal)
                {
                    await Service.FilterValueRemoveAsync(fv);
                }

                await Service.FiltersListRemoveAsync(row);

                return AjaxOperationResult.Success(new { success = true });
            }
            catch (Exception err)
            {
                return AjaxOperationResult.Error(err.Message);
            }
        }
    }
}
