using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MentolVKS.Model.Filters.Dto
{
    /// <summary>
    /// Исключённые поля UI-таблиц из поиска, фильтрации, сортировки. Поля можно посмотреть в таблицах БД dbo.TableColumnSettings и dbo.FilterColumnsList.
    /// </summary>
    public enum ExcludeField
    {
        /// <summary>
        /// Нет поля.
        /// </summary>
        [Display(Name = nameof(None))]
        None = 0,

        /// <summary>
        /// Колонка в UI "Действия".
        /// </summary>
        [Display(Name = nameof(Actions))]
        Actions,

        /// <summary>
        /// Колонка в UI "Check".
        /// </summary>
        [Display(Name = nameof(Check))]
        Check,

        /// <summary>
        /// Колонка в UI "CheckBox".
        /// </summary>
        [Display(Name = nameof(CheckBox))]
        CheckBox,

        /// <summary>
        /// Колонка в UI "CheckBox".
        /// </summary>
        [Display(Name = nameof(Exec))]
        Exec,

        /// <summary>
        /// Колонка в UI "LimitIsSet".
        /// </summary>
        [Display(Name = nameof(LimitIsSet))]
        LimitIsSet,

        /// <summary>
        /// Колонка в UI "LimitYear".
        /// </summary>
        [Display(Name = nameof(LimitYear))]
        LimitYear,

        /// <summary>
        /// Колонка в UI "LimitMonth".
        /// </summary>
        [Display(Name = nameof(LimitMonth))]
        LimitMonth,

        /// <summary>
        /// Колонка типа boolean
        /// </summary>
        [Display(Name = nameof(IsActive))]
        IsActive,

        /// <summary>
        /// Колонка в UI "Дата" 
        /// </summary>
        [Display(Name = nameof(DateRecord))]
        DateRecord,

        /// <summary>
        /// Колонка в UI "Декрет" 
        /// </summary>
        [Display(Name = nameof(MaternityLeave))]
        MaternityLeave
    }
}
