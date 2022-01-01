using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.ViewModel
{
    /// <summary>
    ///     DTO передающая связь таблицы в БД, сущности в коде и Vue таблицы 
    /// </summary>
    public class TableEntityConnectionDto
    {
        /// <summary>
        ///     Таблица во Vue
        /// </summary>
        public string VueTableName { get; set; }

        /// <summary>
        ///     Сущность в коде
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        ///     Сущность в базе данных
        /// </summary>
        public string DbTableName { get; set; }

    }
}
