using MentolVKS.Model.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.BaseModel
{
    /// <summary>
    /// Пользователи системы
    /// </summary>
    public class AspNetUser : TableBasedEntityBase
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// TODO
        /// </summary>
        public int AccessFailedCount { get; set; }
        /// <summary>
        /// TODO
        /// </summary>
        public string ConcurrencyStamp { get; set; }
        /// <summary>
        /// TODO
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// TODO
        /// </summary>
        public bool EmailConfirmed { get; set; }
        /// <summary>
        /// TODO
        /// </summary>
        public bool LockoutEnabled { get; set; }
        /// <summary>
        /// TODO
        /// </summary>
        public DateTimeOffset? LockoutEnd { get; set; }
        /// <summary>
        /// TODO
        /// </summary>
        public string NormalizedEmail { get; set; }
        /// <summary>
        /// TODO
        /// </summary>
        public string NormalizedUserName { get; set; }
        /// <summary>
        /// TODO
        /// </summary>
        public string PasswordHash { get; set; }
        /// <summary>
        /// TODO
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// TODO
        /// </summary>
        public bool PhoneNumberConfirmed { get; set; }
        /// <summary>
        /// TODO
        /// </summary>
        public string SecurityStamp { get; set; }
        /// <summary>
        /// TODO
        /// </summary>
        public bool TwoFactorEnabled { get; set; }
        /// <summary>
        /// TODO
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// TODO
        /// </summary>
        public string UserFullName { get; set; }
        /// <summary>
        /// TODO
        /// </summary>
        public string Post { get; set; }
        /// <summary>
        /// TODO
        /// </summary>
        public string Sid { get; set; }
        /// <summary>
        /// TODO
        /// </summary>
        public string Provider { get; set; } = "Integrated";
        /// <summary>
        /// Ключ доступа к API
        /// </summary>
        public string APIKey { get; set; }
        /// <summary>
        /// Признак необходмости запуска SetRLS при логине
        /// </summary>
        public short? NeedRls { get; set; }
        /// <summary>
        /// TODO
        /// </summary>
        public ICollection<AspNetUserRole> AspNetUserRoles { get; set; } = new HashSet<AspNetUserRole>();
    }
}
