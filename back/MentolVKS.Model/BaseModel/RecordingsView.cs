using MentolVKS.Model.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MentolVKS.Model.BaseModel
{
    /// <summary>
    /// Бронирование - Записи
    /// </summary>
    public class RecordingsView : ViewBasedEntityBase
    {
        /// <summary>
        /// уникальный признак бронирования
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// наименование
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ссылка на конференцию
        /// </summary>
        public string SpaceUri { get; set; }
        /// <summary>
        ///Владелец
        /// </summary>
        public string Owner { get; set; }
        /// <summary>
        ///начало конференции
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? DateStart { get; set; }
        /// <summary>
        /// вычисляемое значение datestart + duration
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? DateEnd { get; set; }
        /// <summary>
        ///длительность конференции
        /// </summary>
        public int? Duration { get; set; }
        /// <summary>
        ///Группа серверов
        /// </summary>
        public String ServersGroupsName { get; set; }

        /// <summary>
        /// Признак просмотра
        /// </summary>
        public bool IsPlay { get; set; }

        /// <summary>
        /// Признак загрузки 
        /// </summary>
        public bool IsDownload { get; set; }

        /// <summary>
        /// Признак Поделиться
        /// </summary>
        public bool IsShare { get; set; }

        /// <summary>
        /// Признак удаления 
        /// </summary>
        public bool IsDelete { get; set; }
    }
}
