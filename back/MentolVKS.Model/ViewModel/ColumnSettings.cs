using MentolVKS.Model.BaseModel;
using MentolVKS.Model.Bases;
using System;


namespace MentolVKS.Model.ViewModel
{
    public class ColumnSettings : ModelBase, IComparable
    {
        /// <summary>
        /// Ширина
        /// </summary>
        private int _width;

        /// <summary>
        /// Тип данных
        /// </summary>
        public string DataType => Template != null && Template.Contains("yyyy") ? "date" : string.Empty;

        /// <summary>
        /// Ширира
        /// </summary>
        public int Width
        {
            get => _width == 0 ? MinWidth : _width;
            set => _width = value;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Шаблон данных
        /// </summary>
        public string Template { get; set; }

        /// <summary>
        /// Минимальная длина
        /// </summary>
        public int MinWidth { get; set; }

        /// <summary>
        /// Имя значения
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Разметка, окружающая заголовок
        /// </summary>
        public string Wrap { get; set; }

        /// <summary>
        /// Отображаемая колонка
        /// </summary>
        public bool IsVisible { get; set; }

        /// <summary>
        /// Пользовательский порядок колонки
        /// </summary>
        public int UserOrder { get; set; }

        /// <summary>
        /// Общий порядок колонки
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Имя поля БД
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// Класс ячейки таблицы
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// Заголовок колонки
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Тип фильтра
        /// </summary>
        public string FilterType { get; set; }

        /// <summary>
        /// Колонка всегда скрыта
        /// </summary>
        public bool AlwaysHidden { get; set; }

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
        /// Определить видимость CheckBox в шапке колонки
        /// </summary>
        public bool VisibleCheckBox { get; set; }

        /// <summary>
        /// Определить Hint колонки
        /// </summary>
        public bool CellsWithoutHint { get; set; }

        /// <summary>
        /// Идентификатор общего компонента на фронте
        /// </summary>
        public string CellRenderer { get; set; }

        #region Implementation of IComparable

        /// <summary>
        /// Сравнение колонок
        /// </summary>
        /// <param name="obj">Сравниваемая колонка</param>
        /// <returns>Результат сравнения</returns>
        public int CompareTo(object obj)
        {
            var setting = (TableColumnSettings)obj;
            return Order.CompareTo(setting.Order);
        }

        #endregion
    }
}

