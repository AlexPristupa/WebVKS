using MentolVKS.Model.BaseModel;
using System.Threading.Tasks;

namespace MentolVKS.Data.Interfaces.Repository
{
    /// <summary>
    /// Интерфейс репозитория для объектов типа <see cref="RecordingVksUsers"/>
    /// </summary>
    public interface IRecordingVksUsersRepository : IRepository<RecordingVksUsers>
    {
        /// <summary>
        /// Получает права на записи по идентификатору записи и идентификатору пользователя
        /// </summary>
        /// <param name="recordingId">Идентификатор записи</param>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns></returns>
        Task<RecordingVksUsers> GetByRecordingIdAndUserIdAsync(int recordingId, int userId);
        /// <summary>
        /// Проверяет наличие прявязки к пользователю.
        /// </summary>
        /// <param name="recordingId"></param>
        /// <returns></returns>
        Task<bool> ChekcByRecrodingIdAsync(int recordingId);
    }
}
