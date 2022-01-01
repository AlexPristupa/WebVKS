using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentolVKS.Model.Auth
{
    /// <summary>
    /// Свойства ролей
    /// </summary>
    public class RoleOptions
    {
        /// <summary>
        /// Список родителей
        /// </summary>
        private readonly List<int?> _parents;

        /// <inheritdoc />
        public RoleOptions()
        {
            _parents = new List<int?>();
        }

        /// <summary>
        /// Список ролей
        /// </summary>
        public List<RoleOptionsItem> Roles { get; set; } = new List<RoleOptionsItem>();

        /// <summary>
        /// Возвращает список дочерних ролей
        /// </summary>
        /// <param name="id">Идентификатор роли</param>
        public IEnumerable<int> GetChilds(int id)
        {
            return Roles
                .Where(r => r.ParentId == id || r.Id == id)
                .Select(r => r.Id)
                .Union(Roles
                    .Where(r => r.ParentId == id)
                    .SelectMany(x => GetChilds(x.Id)))
                .Where(x => x != id);
        }

        /// <summary>
        /// Возвращает список родительских ролей
        /// </summary>
        /// <param name="id">Идентификатор роли</param>
        public IEnumerable<int?> GetParents(int? id)
        {
            var parentId = Roles
                .Where(r => r.Id == id && r.ParentId != null)
                .Select(r => r.ParentId)
                .FirstOrDefault();
            if (parentId == null) return _parents;

            _parents.Add(parentId);

            return GetParents(parentId).ToList();
        }

        /// <summary>
        /// Очищает список родетей
        /// </summary>
        public void Clear()
        {
            _parents.Clear();
        }
    }
}
