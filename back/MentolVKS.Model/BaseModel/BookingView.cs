using MentolVKS.Model.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MentolVKS.Model.BaseModel
{
    /// <summary>
    /// Бронирования
    /// </summary>
    public class BookingView : ViewBasedEntityBase
    {
        public int Id { get; set; }
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        ///  	Владелец
        /// </summary>
        public string Owner { get; set; }
        /// <summary>
        /// Комната
        /// </summary>
        public string  SpaceName { get; set; }
        /// <summary>
        ///URI
        /// </summary>
        public string SpaceUri { get; set; }
        /// <summary>
        /// SpaceID
        /// </summary>
        public int? SpaceId { get; set; }
        /// <summary>
        /// Периодическая
        /// </summary>
        public string Schedule { get; set; }
        /// <summary>
        ///  	Тип конференции
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Текущий статус
        /// </summary>
        public string CurrentStatus { get; set; }
        /// <summary>
        ///Следующий запуск
        /// </summary>
        [Column(TypeName="datetime")]
        public DateTime? NextRun { get; set; }
        /// <summary>
        /// Счетчик
        /// </summary>
        public DateTime? Counter { get; set; }
        /// <summary>
        ///Дата завершения конференции
        /// </summary>
        [Column(TypeName="datetime")]
        public DateTime? DateEnd { get; set; }
    }
}
