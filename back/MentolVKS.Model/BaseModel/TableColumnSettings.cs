
using MentolVKS.Model.Bases;

using System.Collections.Generic;


namespace MentolVKS.Model.BaseModel
{
    /// <summary>
    /// Модель настроек колонок таблицы
    /// </summary>
    public class TableColumnSettings : TableBasedEntityBase
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя таблицы
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Имя колонки
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Значение
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Обёртка значения
        /// </summary>
        public string Wrap { get; set; }

        /// <summary>
        /// Шаблон значения
        /// </summary>
        public string Template { get; set; }

        /// <summary>
        /// Порядковый номер колонки
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Минимальная ширина
        /// </summary>
        public int MinWidth { get; set; }

        /// <summary>
        /// Имя класса ячейки
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// Идентификатор роли
        /// </summary>
        public int? RoleId { get; set; }
        /// <summary>
        /// Признак отмены сортировки
        /// </summary>
        public bool NoSortable { get; set; }

        /// <summary>
        /// Признак отмены изменения ширины колонки
        /// </summary>
        public bool NoResizable { get; set; }

        /// <summary>
        /// Определить колонки с кнопками действия
        /// </summary>
        public bool ActionButton { get; set; }

        /// <summary>
        /// Определить Hint колонки
        /// </summary>
        public bool CellsWithoutHint { get; set; }

        /// <summary>
        /// Определить видимость CheckBox в шапке колонки
        /// </summary>
        public bool VisibleCheckBox { get; set; }

        /// <summary>
        /// Идентификатор общего компонента на фронте
        /// </summary>
        public string CellRenderer { get; set; }

        /// <summary>
        /// Пользовательские настройки
        /// </summary>
        public ICollection<UserTableColumn> UserColumns { get; set; }
    }
}

