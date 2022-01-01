using MentolVKS.Common.TypeExtensions;
using MentolVKS.Model.Filters.Enum;
using MentolVKS.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentolVKS.Controllers.Params
{
    /// <summary>
    /// Возвращает основные параметры для AgGridVue на фронт сайта
    /// </summary>
    public class AgGridVueDtParams
    {
        /// <summary>
        /// Список полей исключаемых из сортировки на стороне клиента. 
        /// К примеру вычисляемые поля в проекциях, как результат работы хр. процедуры в sql server
        /// </summary>
        protected List<string> ExceptionsSorting { get; set; } = new List<string> { ExcludeFieldsSortingFiltering.limitisset.GetDisplayName() };

        /// <summary>
        /// Возвращает шапку таблицы со всеми настройками (сортировки, порядка следования, шириной) 
        /// </summary>
        public List<Dictionary<string, dynamic>> PartyTableGridForVue(Service.Contract.IService service, IEnumerable<ColumnSettings> tableColumnSettings)
        {
            List<Dictionary<string, dynamic>> listObjectForVue = new List<Dictionary<string, dynamic>>();

            foreach (var item in tableColumnSettings)
            {
                Dictionary<string, dynamic> objectForVue = new Dictionary<string, dynamic>
                {
                    // Привести имена полей к camelCase
                    { "nameField", item.ColumnName.ToCamelCase()},
                    { "lngName", item.Title },
                    { "chkbD", item.IsVisible },
                    { "sortable",  !item.NoSortable },
                    { "resizable",  !item.NoResizable },
                    { "actionsColumnsNames",  item.ActionButton },
                    { "cellRenderer", item.CellRenderer },
                    { "cellsWithoutHint", item.CellsWithoutHint },
                    { "visibleCheckBox", item.VisibleCheckBox },
                    { "filter", true },
                    { "userOrderColumn", item.UserOrder },
                    { "width", item.Width },
                    { "filterType", item.FilterType },
                    { "minWidth", item.MinWidth },
                    { "isVisibleHint", item.ColumnName != "choice" }

                };
                listObjectForVue.Add(objectForVue);
            }

            return listObjectForVue;
        }

        /// <summary>
        /// Получение данных для AgGridVue
        /// </summary>
        /// <param name="filteredData">Данные для таблицы</param>
        /// <param name="totalCount">Количество записей</param>
        /// <returns>Данные набора данных полученных в запросе</returns>
        public dynamic GetNamedDTObject(List<object> filteredData, int totalCount)
        {
            return new
            {
                recordsTotal = totalCount,
                data = filteredData.ToList()
            };
        }
    }
}
