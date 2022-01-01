using MentolVKS.Model.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.BaseModel
{
    /// <summary>
    /// Модель связки пользователя с ролью
    /// </summary>
    public class AspNetUserRole : TableBasedEntityBase
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Идентификатор роли
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Роль
        /// </summary>
        public AspNetRole Role { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public AspNetUser User { get; set; }
    }
}
