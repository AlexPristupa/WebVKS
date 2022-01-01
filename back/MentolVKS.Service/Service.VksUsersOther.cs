using MentolVKS.Model.BaseModel;
using MentolVKS.Service.Contract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MentolVKS.Service
{
    public partial class Service : IService
    {
        /// <inheritdoc />
        public async Task<List<SelectDirectoryView>> GetVksUsersOtherSelectDirectoryAsync(string search, int limit)
        {
            return await UnitOfWork.VksUsersOtherRepository.GetSelectDirectoryAsync(search, limit);
        }

        /// <inheritdoc />
        public async Task<VksUsersOther> GetVksUsersOtherByIdAsync(int id)
        {
            return await UnitOfWork.VksUsersOtherRepository.GetByIdAsync(id);
        }
    }
}