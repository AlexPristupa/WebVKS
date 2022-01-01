using MentolVKS.Data.EF.Bases;
using MentolVKS.Data.Interfaces;
using MentolVKS.Data.Interfaces.Repository;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentolVKS.Data.EF.Repository
{
    /// <summary>
    /// Реализация интерфейса репозитория <see cref="IVksUsersOtherRepository"/>
    /// </summary>
    internal class VksUsersOtherRepository : TableBasedEntityRepositoryBase<VksUsersOther>, IVksUsersOtherRepository
    {
        /// <inheritdoc />
        public VksUsersOtherRepository(DataContext context, IColumnMappingConfiguration mappings) : base(context, mappings)
        {
        }

        #region Implementation of IVksUsersOtherRepository

        /// <inheritdoc/>
        public async Task<List<SelectDirectoryView>> GetSelectDirectoryAsync(string search, int limit)
        {
            return await DbSet                
                .Select(c => new SelectDirectoryView
                {
                    Id = c.Id,
                    Name = c.Uri + ' ' + c.Name + " (" + c.Email + ")"
                })
                .Where(c => c.Name.ToLower().Contains(search.ToLower()))
                .OrderBy(c => c.Name)
                .Take(limit)
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<VksUsersOther> GetByUriAsync(string uri)
        {
            return await DbSet.FirstOrDefaultAsync(c => c.Uri == uri && !string.IsNullOrEmpty(c.Uri));
        }

        public async Task<VksUsersOther> GetByEmptyUriAndEmailAsync(string email)
        {
            return await DbSet.FirstOrDefaultAsync(c => string.IsNullOrEmpty(c.Uri) && c.Email == email);
        }

        public async Task<VksUsersOther> GetByUriAndNotId(string uri, int id)
        {
            return await DbSet.FirstOrDefaultAsync(c => c.Uri == uri && c.Id != id);
        }

        #endregion
    }
}
