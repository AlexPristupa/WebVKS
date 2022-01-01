using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MentolVKS.Model.Enums
{
    public enum LogTypes
    {
        /// <summary>
        /// Система
        /// </summary>
        [Display(Name = "Система")]
        System = 1,
        /// <summary>
        /// Журнал
        /// </summary>
        [Display(Name = "Журнал")]
        LOG = 2,
        /// <summary>
        /// Пользователи   
        /// </summary>
        [Display(Name = "Пользователи")]
        USERS = 3,
        /// <summary>
        /// Роли    
        /// </summary>
        [Display(Name = "Роли")]
        ROLES = 4,
        /// <summary>
        /// Бронирования   
        /// </summary>
        [Display(Name = "Бронирования")]
        BOOKING = 5,
        /// <summary>
        /// Комнаты 
        /// </summary>
        [Display(Name = "Комнаты")]
        ROOMS = 6,
        /// <summary>
        /// Записи  
        /// </summary>
        [Display(Name = "Записи ")]
        RECORDS = 7,
        /// <summary>
        /// Профили пользователей  
        /// </summary>
        [Display(Name = "Профили пользователей")]
        USERPROFILES = 8,
        /// <summary>
        /// Группы комнат   
        /// </summary>
        [Display(Name = "Группы комнат")]
        GROUPSOFROOMS = 9,
        /// <summary>
        /// Хранилища записей   
        /// </summary>
        [Display(Name = "Хранилища записей")]
        RECORDSTORES = 10,
        /// <summary>
        /// Exchange    
        /// </summary>
        [Display(Name = "Exchange")]
        EXCHANGE = 11,
        /// <summary>
        /// Сервера 
        /// </summary>
        [Display(Name = "Сервера")]
        SERVERS = 12,
        /// <summary>
        /// Группы 
        /// </summary>
        [Display(Name = "Группы")]
        GROUPS = 13,
        /// <summary>
        /// Отчеты  
        /// </summary>
        [Display(Name = "Отчеты ")]
        REPORTS = 14,
        /// <summary>
        /// Рассылка отчетов    
        /// </summary>
        [Display(Name = "Рассылка отчетов")]
        DISTRIBUTIONOFREPORTS = 15
    }
}
