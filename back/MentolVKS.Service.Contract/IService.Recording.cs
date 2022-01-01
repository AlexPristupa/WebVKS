using MentolVKS.Model.BaseModel;
using System.Threading.Tasks;

namespace MentolVKS.Service.Contract
{
    public partial interface IService
    {
        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteRecordingAsync(int id);

        Task<Recording> GetRecordingByIdAsync(int id);
    }
}
