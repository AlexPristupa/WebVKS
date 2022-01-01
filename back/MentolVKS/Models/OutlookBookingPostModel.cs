using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentolVKS.Models
{
    public class OutlookBookingPostModel
    {
        /// <summary>
        /// значение из интерфейса встречи "Тема"
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// значение из интерфейса встречи "Место"
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        ///значение из интерфейса встречи "Время начала"
        /// </summary>
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int Duration { get; set; }
        public int TimeZoneMinute { get; set; }
        /// <summary>
        /// задачется в интерфейсе, наша настройка параметра "Тип подключения"
        /// </summary>
        public int ConnectionTypeId { get; set; }
        public int OpenConferenceBefore { get; set; }
        public string Emails { get; set; }
        /// <summary>
        /// ICS файл для разбора расписания
        /// </summary>
        public string Ics { get; set; }
        /// <summary>
        /// Уникальный ID календаря
        /// </summary>
        public string Uid { get; set; }
        public string Organizer { get; set; }
        public string TimeZoneName { get; set; }
    }
}
