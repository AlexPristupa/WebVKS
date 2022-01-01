using MentolVKS.Model.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.BaseModel
{
    /// <summary>
    /// Страницы в системе.
    /// </summary>
    public class AspNetRole : TableBasedEntityBase
    {
        /// <summary>
        /// Идентификатор страницы в системе (таблица dbo.AspNetRoles).
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Хэш
        /// </summary>
        public string ConcurrencyStamp { get; set; }

        /// <summary>
        /// Имя страницы в системе.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Нормализованное имя
        /// </summary>
        public string NormalizedName { get; set; }

        /// <summary>
        /// Отображаемое имя страницы.
        /// </summary>
        public string ViewName { get; set; }

        /// <summary>
        /// Идентификатор родительского элемента
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Родительский элемент
        /// </summary>
        //public AspNetRole Parent { get; set; }

        /// <summary>
        /// Список дочерних элементов
        /// </summary>
        //public ICollection<AspNetRole> Children { get; set; } = new HashSet<AspNetRole>();

        /// <summary>
        /// Список связей с пользователями
        /// </summary>
        public ICollection<AspNetUserRole> AspNetUserRoles { get; set; } = new List<AspNetUserRole>();

        /// <summary>
        /// Связные страницы
        /// </summary>
        public ICollection<AspNetTreePage> AspNetTreePages { get; set; } = new HashSet<AspNetTreePage>();
    }
}
