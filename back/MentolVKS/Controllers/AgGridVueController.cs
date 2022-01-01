using MentolVKS.Common;
using MentolVKS.Controllers.Params;
using MentolVKS.Model.Filters;
using MentolVKS.Model.Filters.Dto;
using MentolVKS.Service.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentolVKS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgGridVueController : ApiControllerBase
    {
        private ILogger _logger;

        public AgGridVueController(IService service, ILogger<AgGridVueController> logger) : base(service) {
            _logger = logger;
        }
        /// <summary>
        /// Получить определение полей для Ag-Grid-Vue
        /// </summary>
        /// <param name="TableName">Имя таблицы</param>
        /// <returns></returns>

        [HttpGet("GetNameFieldsAgGridVue")]
        public async Task<AjaxOperationResult> GetNameFieldsAgGridVue(string TableName = "user")
        {
            try
            {
                var paramsAgGridVue = new AgGridVueDtParams();
                var user = await GetCurrentUserAsync();
                var tableColumnSettings = await Service.UserTableColumnsGetSettingsByTableNameAsync(TableName, user.Id);
                var objectFromVue = paramsAgGridVue.PartyTableGridForVue(Service, tableColumnSettings);

                return AjaxOperationResult.Success(objectFromVue);
            }
            catch (Exception err)
            {
                return AjaxOperationResult.Error(err.Message);
            }
        }

        /// <summary>
        /// Устанавливает видимость колонок в гриде
        /// </summary>
        /// <param name="columnsFrontEnd">наименование видимых колонок таблицы</param>
        [HttpPost("postSettingVisibleColumnsAgGridVue")]
        public async Task<AjaxOperationResult> PostSettingVisibleColumnsAgGridVue([FromBody] Dictionary<string, List<string>> columnsFrontEnd)
        {
            try
            {
                var user = await GetCurrentUserAsync();

                foreach (string key in columnsFrontEnd.Keys)
                {
                    var columnsId = new List<int>();
                    var tableColumnSettings = Service.UserTableColumnsGetSettingsByTableNameAsync(key, user.Id).Result.ToArray();
                    var columnsFrontEndArray = columnsFrontEnd[key];
                    foreach (var item in columnsFrontEndArray)
                    {
                        var colId = tableColumnSettings.Where(x => x.ColumnName.Equals(item, StringComparison.OrdinalIgnoreCase)).FirstOrDefault().Id;
                        columnsId.Add(colId);
                    }
                    try
                    {
                        await Service.UserTableColumnsSaveColumnsAsync(key, user.Id, columnsId.ToArray());
                    }
                    catch (Exception ex)
                    {
                        return AjaxOperationResult.Error(ex.Message);
                    }
                }

                return AjaxOperationResult.Success();
            }
            catch (Exception err)
            {
                return AjaxOperationResult.Error(err.Message);
            }
        }

        /// <summary>
        /// Устанавливает ширину колонок
        /// </summary>
        /// <param name="columnsFrontEnd">колонка с заданной шириной</param>
        [HttpPost("postSettingWidthColumnsAgGridVue")]
        public async Task<AjaxOperationResult> PostSettingWidthColumnsAgGridVue([FromBody] Dictionary<string, Dictionary<string, int>> columnsFrontEnd)
        {
            try
            {
                if (columnsFrontEnd == null)
                {
                    return AjaxOperationResult.Error("Список колонок пуст");
                }

                var user = await GetCurrentUserAsync();

                foreach (var k in columnsFrontEnd.Keys)
                {
                    var columnsFrontEndArray = columnsFrontEnd[k];
                    var tableColumnSettings = await Service.UserTableColumnsGetSettingsByTableNameAsync(k, user.Id);

                    // Проверка для тех таблиц, которые не хранятся в БД
                    if (tableColumnSettings.Count() > 0)
                    {
                        foreach (string key in columnsFrontEndArray.Keys)
                        {
                            var settings = tableColumnSettings.FirstOrDefault(x => x.ColumnName.Equals(key, StringComparison.OrdinalIgnoreCase));
                            if (columnsFrontEndArray[key] < settings.MinWidth)
                            {
                                await Service.UserTableColumnsSaveTableColumnWidthAsync(user.Id, settings.Id, settings.MinWidth);
                            }
                            else
                            {
                                await Service.UserTableColumnsSaveTableColumnWidthAsync(user.Id, settings.Id, columnsFrontEndArray[key]);
                            }
                        }
                    }
                }

                return AjaxOperationResult.Success();
            }
            catch (Exception err)
            {
                return AjaxOperationResult.Error(err.Message);
            }
        }

        /// <summary>
        /// Возвращает список ненайденых элементов фильтра
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="tableName"></param>
        /// <param name="checkList"></param>
        /// <returns></returns>
        [RequestFormSizeLimit(4000)]
        [HttpPost]
        [Route("GetNotFoundColumnValue")]
        public async Task<AjaxOperationResult> GetNotFoundColumnValue([FromBody] SelectColumnFilter ob)
        {
            var w = ob;
            // if (addParameters == null)
            var addParameters = new string[0];
            FilterParametrsDto columnFilters = null;
            var user = await GetCurrentUserAsync();
            List<string> notFound = new List<string>();
            List<string> found = new List<string>();

            try
            {
                List<string> rows = (await Service.ColumnForStringFilterGetValueAsync(-1, ob.ColumnName, ob.TableName, user.Id, addParameters, columnFilters))
                    .Select(c => c.Value).ToList();

                notFound = ob.CheckList.Except(rows).ToList();
                found = ob.CheckList.Intersect(rows).ToList();
            }
            catch (Exception)
            {
                //TODO LOG
                //Если неудалось проверить совпадение возвращаем пришедший массив.
                found = ob.CheckList.ToList();
                notFound = new List<string>();
            }

            return AjaxOperationResult.Success(new { NotFound = notFound, Found = found });
        }


    }
}
