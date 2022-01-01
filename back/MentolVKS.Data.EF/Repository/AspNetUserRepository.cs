using MentolVKS.Common;
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
    /// Реализация интерфейса репозитория <see cref="IAspNetUserRepository"/>
    /// </summary>
    public class AspNetUserRepository : TableBasedEntityRepositoryBase<AspNetUser>, IAspNetUserRepository
    {
        /// <inheritdoc />
        public AspNetUserRepository(DataContext context, IColumnMappingConfiguration mappings) : base(context, mappings)
        {
        }

        #region Implementation of IAspNetUserRepository

        /// <inheritdoc />
        public async Task<AspNetUser> GetByNameAsync(string name)
        {
            return await DbSet.FirstOrDefaultAsync(c => c.NormalizedUserName == name.ToUpper());
        }

        /// <inheritdoc />
        public async Task SetUserRlsAsync(int userId)
        {
            if (!Context.EnableRls) return;

            await CallProcedure("dbo.setrls", new object[] { userId });
        }

        /// <inheritdoc />
        public async Task SetLdapUserDefaultRlsAsync(int userId)
        {
            if (!Context.EnableRls) return;

            await CallProcedure("dbo.SetDefaultRLS", new object[] { userId });
        }

        /// <inheritdoc />
        public async Task<List<SelectDirectoryView>> GetSelectDirectoryAsync(string search, int limit)
        {
            return await DbSet
                .Where(c => c.UserName != "secadmin")
                .Select(c => new SelectDirectoryView
                {
                    Id = c.Id,
                    Name = c.UserFullName + " (" + c.Email + ")"
                })
                .Where(c => c.Name.ToLower().Contains(search.ToLower()))
                .OrderBy(c => c.Name)
                .Take(limit)
                .ToListAsync();
        }

        public bool CheckUserPassword(string password, string hashPassword)
        {
            PasswordHasher hashPass = new PasswordHasher();

            return hashPass.VerifyHashedPassword(hashPassword, password);          
        }

        #endregion
    }
}
