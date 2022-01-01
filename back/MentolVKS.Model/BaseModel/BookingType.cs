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
    public class BookingType : TableBasedEntityBase
    {
        public int Id { get; set; }
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string PrivateName { get; set; }
    }
}
