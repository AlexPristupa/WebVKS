using MentolVKS.Model.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.Interfaces
{
    /// <summary>
    /// Модель сопоставления имени таблицы с сущностью БД
    /// </summary>

    public interface ITableNameToModel
    {
        /// <summary>
        /// Имя таблицы
        /// </summary>
        string TableName { get; set; }
        /// <summary>
        /// Список полей не участвующих в запросах UNIT ALL 
        /// </summary>
        IEnumerable<string> ExcludeFields { get; set; }
        /// <summary>
        /// Тип данных модели
        /// </summary>        
        EntityBase Model { get; set; }
        /// <summary>
        /// Словарь сопоставления навигационных свойств с именами заголовков таблицы для формирования Select, потом используется в критериях для поиска и отбора
        /// </summary>
        public IEnumerable<string> SelectTitle { get; set; }
    }
}
