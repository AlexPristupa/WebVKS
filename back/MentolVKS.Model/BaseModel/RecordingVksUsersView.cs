using MentolVKS.Model.Bases;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentolVKS.Model.BaseModel
{
    /// <summary>
    /// Бронирование - Записи
    /// </summary>
    public class RecordingVksUsersView : ViewBasedEntityBase
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор прав на запись
        /// </summary>
        public int RecordingVksUsersId { get; set; }

        /// <summary>
        /// Имя пользователя (полное имя + email)
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Признак просмотра
        /// </summary>
        public bool IsPlay { get; set; }

        /// <summary>
        /// Признак загрузки 
        /// </summary>
        public bool IsDownload { get; set; }

        /// <summary>
        /// Дата записи
        /// </summary>
        [Column(TypeName="datetime")]
        public DateTime DateRecord { get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int UserId { get; set; }
    }
}
