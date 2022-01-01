using MentolVKS.Model.BaseModel;
using MentolVKS.Service.Contract;
using System.Linq;
using System.Threading.Tasks;

namespace MentolVKS.Service
{
    public partial class Service : IService
    {
        /// <inheritdoc />
        public async Task<NfsServers> NfsServersGetFirstAsync()
        {
            var allData = await UnitOfWork.NfsServersRepository.AllAsync();

            return allData.FirstOrDefault();
        }
    }
}