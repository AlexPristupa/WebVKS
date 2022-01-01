using MentolVKS.Model.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.BaseModel
{
    public class SpacesView : ViewBasedEntityBase
    {
        /// <summary>
        /// уникальный признак комнаты
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Группа серверов
        /// </summary>
        public string ServersGroupsName { get; set; }
        /// <summary>
        /// ссылка на конференцию
        /// </summary>
        public string Uri { get; set; }
        /// <summary>
        /// наименование комнаты
        /// </summary>
        public string Name { get; set; }
    }
}
