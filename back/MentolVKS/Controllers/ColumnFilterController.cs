using MentolVKS.Common;
using MentolVKS.Model.Filters.Dto;
using MentolVKS.Service.Contract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentolVKS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColumnFilterController : ApiControllerBase
    {
        public ColumnFilterController(IService service) : base(service)
        {
        }

        /// <summary>
        /// Применение колоночного фильтра на основе DTO полученного с фронта
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /api/ColumnFilter
        ///     {
        ///     
        ///         TableName: 'tar_notify_log',   Имя таблицы
        ///         Page: 1,                                Номер страницы для пагинации
        ///         Limit: 25,                              Размер страницы пагинации
        ///         TableSearchBy: 'ищем что-нибудь',       Значение быстрого поиска по колонкам
        ///         OrderBy: 'asc',                         Направление сортировки
        ///         SortField: 'DateRecord',                Сортируемое поле
        ///         DateStart: '29.10.2020',                Начальная дата фильтрации по диапазону дат
        ///         DateFinish: '30.10.2020',               Конечная дата фильтрации по диапазону дат  
        ///                                                 Массив колоночных фильтров
        ///         Filters: [
        ///             {"tableName": "tar_notify_log", "nameField": "nameEmployee", "filterType": "select", "valuesField": ["6666"] },
        ///             {"tableName": "tar_notify_log", "nameField": "processingDate","filterType": "date","valuesField": [
        ///                                 {"selectFirstContains": ">=", "valueFirstDateFilter": "31.10.2020", "timerFirstFilter": "00:00" }, 
        ///                                 {"selectSecondContains": "=", "valueSecondDateFilter": "01.11.2020", "timeSecondFilter": "23:59" }]},        
        ///             {"tableName": "tar_notify_log","nameField": "attemptCount","filterType": "integer", "valuesField": [
        ///                                 {
        ///                                     "operand": ">",
        ///                                     "compareValue": 1
        ///                                 }]
        ///             },
        ///             {"tableName": "tar_notify_log", "nameField": "info", "filterType": "string", "valuesField": [
        ///                                 {
        ///                                     "operand": "*~*",
        ///                                     "compareValue": "текст"
        ///                                 }]
        ///             }
        ///         ],
        ///         ChkCurrentPageExport: true              Признак, определяющий выгрузку текущей страницы TableGridGeneral в внешний файл, иначе выгружается весь набор данных
        ///                                                 Массив расширенных фильтров
        ///         ExtensionFilters:  [
        ///             {"tableName": "invExtensionPStatus", "nameField": "statusid", "filterType": "select", "valuesField": [1]},
        ///             {"tableName":"invExtensionPStatus","nameField":"datefrom","filterType":"date","valuesField":[
        ///                         {"selectFirstContains": "=", "valueFirstDateFilter": "21.04.2021", "timerFirstFilter": "00:00" }, 
        ///                         { "selectSecondContains": "=", "valueSecondDateFilter": "21.04.2021", "timeSecondFilter": "23:59" }]},
        ///         ],
        ///     }
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
        /// <param name="listQuery">dto</param>
        /// <returns>Возвращаем отфильтрованный, отсортированный набор данных с применением пагинации в типе AjaxOperationResult</returns>
        /// <response code="200">Возвращает созданный AjaxOperationResult</response>

        [HttpPost]
        public async Task<AjaxOperationResult> Get([FromBody] FilterQuery listQuery)
        {
            try
            {
                var validateResult = FilterQuery.Validate(listQuery);
                if (!validateResult.IsValid)
                    return AjaxOperationResult.Warning(validateResult.ErrorMessage);

                var result = await PreLoadFactoryAsync(listQuery);

                return AjaxOperationResult.Success(result);
            }
            catch (Exception err)
            {
                return AjaxOperationResult.Error(err.Message);
            }
        }

        /// <summary>
        /// Получить связку таблицы с Vue, базы данных БД и сущности кода
        /// </summary>
        /// <response code="200">Возвращает id добавленной структуры</response>
        /// <response code="401">Пользователь не авторизован или не имеет прав доступа для соверщения данного действия</response>  
        [HttpGet("EntityList")]
        public async Task<AjaxOperationResult> EntityList()
        {
            try
            {
                var result = await Service.AgGridGetAllConnectionsAsync();
                return AjaxOperationResult.Success(result);
            }
            catch (Exception err)
            {
                return AjaxOperationResult.Error(err.Message);
            }
        }
    }
}
