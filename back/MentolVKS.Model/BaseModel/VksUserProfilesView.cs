using MentolVKS.Model.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MentolVKS.Model.BaseModel
{
    /// <summary>
    /// Профили пользователей
    /// </summary>
    public class VksUserProfilesView : ViewBasedEntityBase
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
        ///Группа серверов
        /// </summary>
        public string ServersGroupsName { get; set; }
    }
}
