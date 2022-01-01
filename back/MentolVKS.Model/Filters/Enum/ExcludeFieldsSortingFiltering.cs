using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MentolVKS.Model.Filters.Enum
{
    /// <summary>
    /// Исключённые поля UI-таблиц из поиска, фильтрации, сортировки. Поля можно посмотреть в таблицах БД dbo.TableColumnSettings и dbo.FilterColumnsList.
    /// </summary>
    public enum ExcludeFieldsSortingFiltering
    {
        /// <summary>
        /// Исключаемое поле из сортировки и фильтрации - установленные лимиты
        /// </summary>
        [Display(Name = nameof(limitisset))]
        limitisset = 0,


    }
}
