using System;
using System.Collections.Generic;
using System.Text;
using MentolVKS.Model.Bases;

namespace MentolVKS.Model.BaseModel
{
    /// <summary>
    /// Сохранение настроек колонки по определенному пользователю
    /// </summary>
    public class UserTableColumn : TableBasedEntityBase
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Идентификатор таблицы TableColumnSettings
        /// </summary>
        public int TableColumnId { get; set; }
        /// <summary>
        /// Порядковый номера в сортировки 
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// Сохраняемая ширина колонки
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Настройки таблицы
        /// </summary>
        public TableColumnSettings Settings { get; set; }
    }
}
