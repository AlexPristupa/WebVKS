using MentolVKS.Model.BaseModel;
using MentolVKS.Service.Contract;
using System.Threading.Tasks;

namespace MentolVKS.Service
{
    public partial class Service : IService
    {
        /// <inheritdoc />
        public async Task<Services> ServicesGetByNameAsync(string name)
        {
            return await UnitOfWork.ServicesRepository.GetByNameAsync(name);
        }
    }
}