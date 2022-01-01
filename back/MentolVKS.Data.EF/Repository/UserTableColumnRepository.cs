using MentolVKS.Data.EF.Bases;
using MentolVKS.Data.Interfaces;
using MentolVKS.Data.Interfaces.Repository;
using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentolVKS.Data.EF.Repository
{
    public class UserTableColumnRepository : TableBasedEntityRepositoryBase<UserTableColumn>, IUserTableColumnRepository
    {
        public UserTableColumnRepository(DataContext context, IColumnMappingConfiguration mappings) : base(context, mappings)
        {
        }

        /// <inheritdoc />
        public async Task<IEnumerable<UserTableColumn>> GetAllByTableNameAndUserIdAsync(string tableName, int userId)
        {
            return await DbSet.Where(u => u.UserId == userId && u.Settings.TableName.Equals(tableName)).ToListAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<UserTableColumn>> GetAllByColumnsAndUserIdAsync(int[] columns, int userId)
        {
            return await DbSet.Where(u => columns.Contains(u.TableColumnId) && u.UserId == userId).ToListAsync();
        }

        /// <inheritdoc />
        public async Task<UserTableColumn> GetByColumnIdAndUserIdAsync(int columnId, int userId)
        {
            return await DbSet.Include(c => c.Settings).FirstOrDefaultAsync(u => u.TableColumnId == columnId && u.UserId == userId);
        }
    }
}
