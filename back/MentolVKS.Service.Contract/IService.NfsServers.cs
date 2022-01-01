using MentolVKS.Model.BaseModel;
using System.Threading.Tasks;

namespace MentolVKS.Service.Contract
{
    public partial interface IService
    {
        /// <summary>
        /// Получает первую запись о сервере nfs
        /// </summary>
        Task<NfsServers> NfsServersGetFirstAsync();
    }
}
