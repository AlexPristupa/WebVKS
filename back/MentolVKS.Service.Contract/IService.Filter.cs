using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using MentolVKS.Model.Filters.Dto;
using MentolVKS.Model.Filters.Enum;
using MentolVKS.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MentolVKS.Service.Contract
{
    public partial interface IService
    {
        /// <summary>
        /// Получить НД из БД на основе <see cref="FilterQuery"/>, полученного с фронта
        /// </summary>
        /// <param name="listQuery"> <see cref="FilterQuery"/> с фронта</param>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns></returns>
        Task<dynamic> AgGridVueGetData(FilterQuery listQuery, int userId);

        /// <summary>
        /// Получить НД из БД на основе DTO, полученного с фронта
        /// </summary>
        /// <param name="listQuery"> <see cref="FilterQuery"/> с фронта</param>
        /// <param name="listFields">Поля быстрого поиска по значению TableSearchBy для построения UNION ALL</param>
        /// <returns></returns>
        Task<dynamic> AgGridVueGetData(FilterQuery listQuery, List<string> listFields = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="userId"></param>
        /// <param name="setDefault"></param>
        /// <returns></returns>
        Task<IEnumerable<ColumnSettings>> UserTableColumnsGetSettingsByTableNameAsync(string tableName, int userId, bool setDefault = true);

        /// <summary>
        /// Получить связку таблицы с Vue, базы данных БД и сущности кода
        /// </summary>
        /// <returns></returns>
        Task<List<TableEntityConnectionDto>> AgGridGetAllConnectionsAsync();

        /// <summary>
        /// Сохраняет колонки таблицы для пользователя
        /// </summary>
        /// <param name="tableName">Имя таблицы</param>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="columns">Список идентификаторов колонок</param>
        /// <returns>Результат выполнения</returns>
        Task<SaveColumnResult> UserTableColumnsSaveColumnsAsync(string tableName, int userId, int[] columns);

        /// <summary>
        /// Сохраняет ширину колонки
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="columnId">Идентификатор колонки</param>
        /// <param name="width">Ширина</param>
        /// <returns>Результат выполнения</returns>
        Task<bool> UserTableColumnsSaveTableColumnWidthAsync(int userId, int columnId, int width);

        /// <summary>
        /// Возвращает список значений для фильтра "Выбор"
        /// </summary>
        /// <param name="filterId">Идентификатор фильтра</param>
        /// <param name="colName">Имя колонки</param>
        /// <param name="tableName">Имя таблицы</param>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="parameters">Список дополнительных параметров</param>
        /// <param name="columnFilter">Данные фильтра</param>
        /// <param name="values">Список доступных значений</param>
        /// <param name="dateFrom">Дата начала</param>
        /// <param name="dateTo">Дата окончания</param>
        /// <param name="siteId">Идентификатор станции</param>
        /// <returns>Списко значений для фильтра</returns>
        Task<IEnumerable<ColumnForStringFilter>> ColumnForStringFilterGetValueAsync(int filterId, string colName, string tableName, int userId,
            object[] parameters, FilterParametrsDto columnFilter, string[] values = null, DateTime? dateFrom = null, DateTime? dateTo = null, int siteId = -1);

        /// <summary>
        /// Возвращает список значений для фильтра "Выбор"
        /// </summary>
        /// <param name="filterId">Идентификатор фильтра</param>
        /// <param name="colName">Имя колонки</param>
        /// <param name="tableName">Имя таблицы</param>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="parameters">Список дополнительных параметров</param>
        /// <param name="columnFilter">Данные фильтра</param>
        /// <param name="values">Список доступных значений</param>
        /// <param name="dateFrom">Дата начала</param>
        /// <param name="dateTo">Дата окончания</param>
        /// <returns>Список значений для фильтра</returns>
        Task<(string, IEnumerable<ColumnForStringFilter>)> ColumnForStringFilterGetValueMemberAsync(int filterId, string colName, string tableName, int userId, object[] parameters, FilterParametrsDto columnFilter, DateTime? dateFrom = null, DateTime? dateTo = null);

        /// <summary>
        /// Получить фильтры таблицы для пользователя
        /// </summary>
        /// <param name="tableName">Имя таблицы</param>
        /// <param name="userId">ИД пользователя</param>
        /// <returns>Список фильтров</returns>
        Task<IEnumerable<FilterName>> FilterNameAllByTableAndUserIdAsync(string tableName, int userId);

        Task<FilterTablesList> FilterTablesListGetByTableAsync(string tableName);

        /// <summary>
        /// Добавляет фильтр
        /// </summary>
        /// <param name="filtersList">Добавляемые данные</param>
        Task FiltersListAddAsync(FiltersList filtersList);

        Task<IEnumerable<FilterColumnsList>> FilterColumnsListGetByColumnAndTableAsync(string columnName,
            string tableName);

        Task FiltersToUserLinkAddAsync(FiltersToUserLink filterLink);

        Task<IEnumerable<FilterColumnsList>> FilterColumnsListGetByFilterAndTableAsync(int filterId,
                string tableName);

        Task<IEnumerable<FilterValue>> FilterValueAllByFilterIdAsync(int filterId);
        Task<IEnumerable<FilterValue>> FilterValueAllByFilterAndColumnAsync(int filterId, int columnId);
        Task FilterValueRemoveAsync(FilterValue filterValue);
        Task FilterValueAddAsync(FilterValue filterValue);
        Task<FiltersList> FiltersListGetByIdAsync(int id);
        /// <summary>
        /// Удаляет фильтр
        /// </summary>
        /// <param name="filtersList">Удаляемые данные</param>
        Task FiltersListRemoveAsync(FiltersList filtersList);
        Task<FilterOperationsList> FilterOperationsListGetByOperandAsync(string operand);
        /// <summary>
        /// Получение всех записей по ИД фильтра
        /// </summary>
        /// <param name="filterId">ИД фильтра</param>
        /// <returns>Список записей</returns>
        Task<IEnumerable<FilterColumn>> FilterColumnAllByFilterIdAsync(int filterId);
        Task<IEnumerable<FilterForColumnTypeList>> FilterForColumnTypeListGetTypeFilterAsync(int filterId, string colName,
    string tableName);

        Task<IEnumerable<ColumnForIntegerFilter>> ColumnForIntegerFilterGetByFilterAsync(int filterId, string colName,
    string tableName);
        /// <summary>
        /// Проверяет имя фильтра на уникальность
        /// </summary>
        /// <param name="name">Имя фильтра</param>
        /// <param name="id">Идентификатор</param>
        Task<bool> FiltersListCheckNameUniqueAsync(string name, int id);
        /// <summary>
        /// Сохраняет фильтр
        /// </summary>
        /// <param name="filtersList">Сохраняемые данные</param>
        Task FiltersListSaveAsync(FiltersList filtersList);
    }
}
