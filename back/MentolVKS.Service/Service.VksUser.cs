using MentolVKS.Model.BaseModel;
using MentolVKS.Service.Contract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MentolVKS.Service
{
    public partial class Service : IService
    {
        /// <inheritdoc />
        public async Task<List<SelectDirectoryView>> GetVksUserSelectDirectoryAsync(string search, int limit)
        {
            return await UnitOfWork.VksUserRepository.GetSelectDirectoryAsync(search, limit);
        }

        /// <inheritdoc />
        public async Task<VksUser> GetVksUserByJidAsync(string jid)
        {
            return await UnitOfWork.VksUserRepository.GetByJidAsync(jid);
        }

        /// <inheritdoc />
        public async Task<VksUser> GetVksUserByIdAsync(int id)
        {
            return await UnitOfWork.VksUserRepository.GetByIdAsync(id);
        }
    }
}