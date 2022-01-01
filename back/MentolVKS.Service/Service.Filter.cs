using MentolVKS.Common.TypeExtensions;
using MentolVKS.Data.Interfaces;
using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using MentolVKS.Model.Filters.Dto;
using MentolVKS.Model.Filters.Enum;
using MentolVKS.Model.ViewModel;
using MentolVKS.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentolVKS.Service
{
    public partial class Service : IService
    {
        /// <inheritdoc />
        public async Task<dynamic> AgGridVueGetData(FilterQuery filters, int userId)
        {
            return await UnitOfWork.AgGridVueRepository.GetDataAsync(filters, await GetListFileds(filters.TableName, userId));
        }

        /// <inheritdoc />
        public async Task<dynamic> AgGridVueGetData(FilterQuery listQuery, List<string> listFields = null)
        {
            // Чтобы работал поиск по "tableSearchBy", надо использовать перегрузку AgGridVueGetData(FilterQuery listQuery, int userId).
            listFields ??= new List<string>();

            return await UnitOfWork.AgGridVueRepository.GetDataAsync(listQuery, listFields);
        }

        /// <summary>
        /// Получить список полей для быстрого поиска 
        /// </summary>
        /// <param name="tableName">Имя таблицы</param>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns>Список полей для быстрого поиска</returns>
        private async Task<List<string>> GetListFileds(string tableName, int userId)
        {
            var tableColumnSettings = await UserTableColumnsGetSettingsByTableNameAsync(tableName, userId);

            // Получить список полей для UNION ALL SQL
            List<string> listFields = tableColumnSettings.Select(x => x.ColumnName).ToList();

            //Ag-grid-vue, в какой-то момент начинает дописывать к именам полей "_1"
            return listFields.Select(x => x.Replace("_1", string.Empty)).ToList();
        }

        public async Task<IEnumerable<ColumnSettings>> UserTableColumnsGetSettingsByTableNameAsync(string tableName, int userId,
            bool setDefault = true)
        {
            var tableColumns = await UnitOfWork.TableColumnSettingsRepository.GetByTableNameAsync(tableName);
            var allColumns = tableColumns.Select(s => new ColumnSettings
            {
                Id = s.Id,
                Order = s.Order,
                MinWidth = s.MinWidth,
                Template = s.Template,
                Value = s.Value,
                Wrap = s.Wrap,
                ColumnName = s.ColumnName,
                ClassName = s.ClassName,
                Title = s.Title,
                UserOrder = tableColumns.Count(),
                NoSortable = s.NoSortable,
                NoResizable = s.NoResizable,
                ActionButton = s.ActionButton,
                CellRenderer = s.CellRenderer,
                CellsWithoutHint = s.CellsWithoutHint,
                VisibleCheckBox = s.VisibleCheckBox
            }).ToArray();

            var filterTypes = await UnitOfWork.FilterColumnsListRepository.AllByTableAsync(tableName.Replace("_table", string.Empty));

            foreach (var column in allColumns)
            {
                var filterType = filterTypes.FirstOrDefault(t => t.ColumnName.Is(column.ColumnName));

                if (!filterType?.FilterTypeId.HasValue ?? true) continue;

                var columnType = ((ColumnFiltersType)filterType.FilterTypeId).ToString().ToCamelCase();

                column.FilterType = columnType;
            }

            var columns = (await UnitOfWork.UserTableColumnRepository.GetAllByTableNameAndUserIdAsync(tableName, userId)).ToArray();

            if (columns.Any())
            {
                foreach (var column in allColumns)
                {
                    var userColumn = columns.FirstOrDefault(c => c.TableColumnId == column.Id);
                    if (userColumn == null) continue;

                    column.IsVisible = true;
                    column.UserOrder = userColumn.Order;
                    column.Width = userColumn.Width;
                }

                return SortColumnSettings(allColumns);
            }

            foreach (var column in allColumns)
            {
                column.IsVisible = true;
            }

            if (!setDefault) return SortColumnSettings(allColumns);

            foreach (var settings in allColumns)
            {
                await UnitOfWork.UserTableColumnRepository.AddAsync(new UserTableColumn
                {
                    Order = settings.Order,
                    TableColumnId = settings.Id,
                    UserId = userId,
                    Width = settings.MinWidth
                });
            }

            return SortColumnSettings(allColumns);
        }

        /// <summary>
        /// Сортирует колонки
        /// </summary>
        /// <param name="columnSettings">Данные колонок</param>
        /// <returns>Отсортированный список</returns>
        private static IEnumerable<ColumnSettings> SortColumnSettings(IEnumerable<ColumnSettings> columnSettings)
        {
            return columnSettings.OrderBy(c => c.UserOrder).ThenBy(c => c.Order);
        }

        public async Task<List<TableEntityConnectionDto>> AgGridGetAllConnectionsAsync()
        {
            var models = UnitOfWork.AgGridVueRepository.GetModelFactoryList();
            var databaseCorelations = await UnitOfWork.FilterTablesListRepository.AllAsync();
            var modelsToEntity = databaseCorelations.Join(models, y => y.TableName
                 .ToLower(), z => z.TableName.ToLower(),
                 (w, s) => new TableEntityConnectionDto
                 {
                     DbTableName = w.Dbtable,
                     EntityName = s.Model
                 .GetType().Name,
                     VueTableName = w.TableName
                 })
                       .ToList();

            return modelsToEntity;
        }

        public async Task<SaveColumnResult> UserTableColumnsSaveColumnsAsync(string tableName, int userId, int[] columns)
        {
            if (!columns.Any()) return SaveColumnResult.LastColumn;

            var tableColumns = (await UnitOfWork.TableColumnSettingsRepository.GetByTableNameAsync(tableName)).ToArray();
            var tableColummIdList = tableColumns.Select(t => t.Id).ToArray();

            var requiredColumns = tableColumns.Where(c => !string.IsNullOrEmpty(c.ClassName) && c.ClassName.Contains("required"));

            if (requiredColumns.Any(c => !columns.Contains(c.Id))) return SaveColumnResult.RequiredColumn;

            var userColumns =
                (await UnitOfWork.UserTableColumnRepository.GetAllByColumnsAndUserIdAsync(tableColummIdList, userId))
                .ToArray();

            var order = -1;
            foreach (var column in userColumns)
            {
                column.Order = -1;
            }

            var colList = new List<UserTableColumn>();
            foreach (var columnId in columns)
            {
                order++;
                var column = userColumns.FirstOrDefault(c => c.TableColumnId == columnId);
                if (column == null)
                {
                    var width = tableColumns.FirstOrDefault(r => r.Id == columnId)?.MinWidth ?? 0;
                    colList.Add(new UserTableColumn
                    {
                        Order = order,
                        UserId = userId,
                        TableColumnId = columnId,
                        Width = width
                    });
                }
                else
                {
                    if (column.Order == order) continue;

                    column.Order = order;

                    await UnitOfWork.UserTableColumnRepository.SaveAsync(column);
                }
            }

            var removeColumns = userColumns.Where(c => c.Order == -1).ToArray();

            await UnitOfWork.UserTableColumnRepository.DeleteRangeAsync(removeColumns);
            await UnitOfWork.UserTableColumnRepository.AddRangeAsync(colList);

            return SaveColumnResult.Success;
        }

        public async Task<bool> UserTableColumnsSaveTableColumnWidthAsync(int userId, int columnId, int width)
        {
            bool result;
            var userColumn = await UnitOfWork.UserTableColumnRepository.GetByColumnIdAndUserIdAsync(columnId, userId);
            if (userColumn == null) return false;

            result = Math.Abs(userColumn.Width - width) > 5;
            userColumn.Width = width;

            await UnitOfWork.UserTableColumnRepository.SaveAsync(userColumn);

            return result;
        }

        public async Task<IEnumerable<ColumnForStringFilter>> ColumnForStringFilterGetValueAsync(int filterId, string colName, string tableName, int userId, object[] parameters, FilterParametrsDto columnFilter, string[] values = null, System.DateTime? dateFrom = null, System.DateTime? dateTo = null, int siteId = -1)
        {
            var dataQuery = string.Empty;
            var param = new List<object>();
            var filterTable = await UnitOfWork.FilterTablesListRepository.GetByTableAsync(tableName);
            var dbTable = filterTable?.Dbtable;
            var filterColumnList = (await UnitOfWork.FilterColumnsListRepository.GetColumnValueFromTableAsync(filterId, colName, dbTable)).FirstOrDefault();

            if (filterColumnList == null)
            {
                var temp = await UnitOfWork.FilterColumnsListRepository.GetByColumnAndTableAsync(colName, tableName);
                if (temp.Any() && temp.First() != null && !string.IsNullOrEmpty(temp.First().DataQuery))
                {
                    dataQuery = temp.First().DataQuery;

                    param.Add(null);
                    param.Add(userId);
                    param.AddRange(parameters);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(filterColumnList.DataQuery))
                {
                    var typeId = filterColumnList.FilterTypeId.Value;
                    dataQuery = (await UnitOfWork.FilterForColumnTypeListRepository.GetByIdAsync(typeId)).DataQuery;

                    dataQuery = string.Format(dataQuery, filterId, dbTable, colName );
                }
                else
                {
                    dataQuery = filterColumnList.DataQuery;

                    param.Add(filterId);
                    param.Add(userId);
                }
            }

            if (string.IsNullOrEmpty(dataQuery))
            {
                return await UnitOfWork.ColumnForStringFilterRepository.AllByTableAndColumnAsync(dbTable, colName, dateFrom, dateTo, columnFilter);
            }

            //Для поиска по фильтру через SQL.
            if (columnFilter != null)
            {
                if (string.IsNullOrEmpty(columnFilter.Search))
                    dataQuery = dataQuery.Replace("search", string.Empty);
                else
                    dataQuery = dataQuery.Replace("search", columnFilter.Search);
            }

            return await UnitOfWork.ColumnForStringFilterRepository.AllByQueryAsync(dataQuery, columnFilter, values, param.ToArray());
        }

        public async Task<(string, IEnumerable<ColumnForStringFilter>)> ColumnForStringFilterGetValueMemberAsync(int filterId, string colName, string tableName, int userId, object[] parameters, FilterParametrsDto columnFilter, DateTime? dateFrom, DateTime? dateTo)
        {

            string valueMember = "";

            var dataQuery = string.Empty;
            var param = new List<object>();
            var filterTable = await UnitOfWork.FilterTablesListRepository.GetByTableAsync(tableName);
            var dbTable = filterTable?.Dbtable;
            var filterColumnList = (await UnitOfWork.FilterColumnsListRepository.GetColumnValueFromTableAsync(filterId, colName, dbTable)).FirstOrDefault();

            if (filterColumnList == null)
            {
                var temp = await UnitOfWork.FilterColumnsListRepository.GetByColumnAndTableAsync(colName, tableName);
                if (temp.Any() && temp.First() != null && !string.IsNullOrEmpty(temp.First().DataQuery))
                {
                    dataQuery = temp.First().DataQuery;

                    param.Add(null);
                    param.Add(userId);
                    param.AddRange(parameters);
                }

                valueMember = temp.First().ValueMember;
            }
            else
            {
                if (string.IsNullOrEmpty(filterColumnList.DataQuery))
                {
                    var typeId = filterColumnList.FilterTypeId.Value;
                    dataQuery = (await UnitOfWork.FilterForColumnTypeListRepository.GetByIdAsync(typeId)).DataQuery;

                    dataQuery = string.Format(dataQuery, colName, dbTable, filterId);
                }
                else
                {
                    dataQuery = filterColumnList.DataQuery;

                    param.Add(filterId);
                    param.Add(userId);
                }

                valueMember = filterColumnList.ValueMember;
            }

            if (string.IsNullOrEmpty(dataQuery))
            {
                return (valueMember, await UnitOfWork.ColumnForStringFilterRepository.AllByTableAndColumnAsync(dbTable, colName, dateFrom, dateTo, columnFilter));
            }

            //Для поиска по фильтру через SQL.
            if (columnFilter != null)
            {
                if (string.IsNullOrEmpty(columnFilter.Search))
                    dataQuery = dataQuery.Replace("search", string.Empty);
                else
                    dataQuery = dataQuery.Replace("search", columnFilter.Search);
            }

            return (valueMember, await UnitOfWork.ColumnForStringFilterRepository.AllByQueryAsync(dataQuery, columnFilter, null, param.ToArray()));
        }

        public async Task<IEnumerable<FilterName>> FilterNameAllByTableAndUserIdAsync(string tableName, int userId)
        {
            return await UnitOfWork.FilterNameRepository.AllByTableAndUserIdAsync(tableName, userId);
        }

        public async Task<FilterTablesList> FilterTablesListGetByTableAsync(string tableName)
        {
            return await UnitOfWork.FilterTablesListRepository.GetByTableAsync(tableName);
        }

        /// <inheritdoc/>
        public async Task FiltersListAddAsync(FiltersList filtersList)
        {
            await UnitOfWork.FiltersListRepository.AddAsync(filtersList);
        }

        public async Task<IEnumerable<FilterColumnsList>> FilterColumnsListGetByColumnAndTableAsync(string columnName, string tableName)
        {
            return await UnitOfWork.FilterColumnsListRepository.GetByColumnAndTableAsync(columnName, tableName);
        }

        public async Task FiltersToUserLinkAddAsync(FiltersToUserLink filterLink)
        {
            await UnitOfWork.FiltersToUserLinkRepository.AddAsync(filterLink);
        }

        public async Task<IEnumerable<FilterColumnsList>> FilterColumnsListGetByFilterAndTableAsync(int filterId, string tableName)
        {
            return await UnitOfWork.FilterColumnsListRepository.GetByFilterAndTableAsync(filterId, tableName);
        }

        public async Task<IEnumerable<FilterValue>> FilterValueAllByFilterIdAsync(int filterId)
        {
            return await UnitOfWork.FilterValueRepository.AllByFilterIdAsync(filterId);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<FilterValue>> FilterValueAllByFilterAndColumnAsync(int filterId, int columnId)
        {
            return await UnitOfWork.FilterValueRepository.AllByFilterAndColumnAsync(filterId, columnId);
        }

        /// <inheritdoc />
        public async Task FilterValueRemoveAsync(FilterValue filterValue)
        {
            await UnitOfWork.FilterValueRepository.DeleteAsync(filterValue);
        }

        /// <inheritdoc />
        public async Task FilterValueAddAsync(FilterValue filterValue)
        {
            if (filterValue.FilterId < 0) return;

            await UnitOfWork.FilterValueRepository.AddAsync(filterValue);
        }
        public async Task FiltersListRemoveAsync(FiltersList filtersList)
        {
            await UnitOfWork.FiltersListRepository.DeleteAsync(filtersList);
        }

        public async Task<FiltersList> FiltersListGetByIdAsync(int id)
        {
            return await UnitOfWork.FiltersListRepository.GetByIdAsync(id);
        }

        public async Task<FilterOperationsList> FilterOperationsListGetByOperandAsync(string operand)
        {
            return await UnitOfWork.FilterOperationsListRepository.GetByOperandAsync(operand);
        }

        public async Task<IEnumerable<FilterColumn>> FilterColumnAllByFilterIdAsync(int filterId)
        {
            return await UnitOfWork.FilterColumnRepository.AllByFilterIdAsync(filterId);
        }

        public async Task<IEnumerable<FilterForColumnTypeList>> FilterForColumnTypeListGetTypeFilterAsync(int filterId, string colName, string tableName)
        {
            return await UnitOfWork.FilterForColumnTypeListRepository.GetTypeFilterAsync(filterId, colName, tableName);
        }

        public async Task<IEnumerable<ColumnForIntegerFilter>> ColumnForIntegerFilterGetByFilterAsync(int filterId, string colName, string tableName)
        {
            var row = new List<ColumnForIntegerFilter>();
            var filterTable = await UnitOfWork.FilterTablesListRepository.GetByTableAsync(tableName);

            if (filterTable == null) return row;

            var dbTable = filterTable.Dbtable;
            var filterColumnList = (await UnitOfWork.FilterColumnsListRepository.GetColumnValueFromTableAsync(filterId, colName, dbTable)).FirstOrDefault();

            if (filterColumnList == null) return row;
            var typeId = filterColumnList.FilterTypeId.Value;
            string query = (await UnitOfWork.FilterForColumnTypeListRepository.GetByIdAsync(typeId)).DataQuery;
            var com = string.Format(query, filterId, colName);
            row = (await UnitOfWork.ColumnForIntegerFilterRepository.GetFromSqlAsync(com)).ToList();

            return row;
        }

        public async Task<bool> FiltersListCheckNameUniqueAsync(string name, int id)
        {
            var filters = await UnitOfWork.FiltersListRepository.AllForTableByIdAsync(id);

            var filter = filters.FirstOrDefault(f => f.Id == id);

            return !filters.Any(f => f.FilterName.Is(name) && f.IsCommon == filter.IsCommon && f.Id != id);
        }

        public async Task FiltersListSaveAsync(FiltersList filtersList)
        {
            await UnitOfWork.FiltersListRepository.SaveAsync(filtersList);
        }
    }
}