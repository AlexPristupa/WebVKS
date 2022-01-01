using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MentolVKS.Data.Interfaces.Repository
{
    public interface IUserTableColumnRepository : IRepository<UserTableColumn>
    {
        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<UserTableColumn>> GetAllByTableNameAndUserIdAsync(string tableName, int userId);

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<UserTableColumn>> GetAllByColumnsAndUserIdAsync(int[] columns, int userId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<UserTableColumn> GetByColumnIdAndUserIdAsync(int columnId, int userId);
    }
}
