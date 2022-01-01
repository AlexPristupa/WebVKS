using MentolVKS.Model.BaseModel;
using System.Threading.Tasks;

namespace MentolVKS.Service.Contract
{
    public partial interface IService
    {
        /// <summary>
        /// Добавление прав на записи
        /// </summary>
        /// <param name="item">Добавляемый объект</param>
        /// <returns></returns>
        Task<RecordingVksUsers> AddRecordingVksUsersAsync(RecordingVksUsers item);

        /// <summary>
        /// Редактирование прав на записи
        /// </summary>
        /// <param name="item">Изменяемый объект</param>
        /// <returns></returns>
        Task<RecordingVksUsers> UpdateRecordingVksUsersAsync(RecordingVksUsers item);

        /// <summary>
        /// Удаление прав на записи
        /// </summary>
        /// <param name="id">Идентификатор удаляемого объекта</param>
        /// <returns></returns>
        Task DeleteRecordingVksUsersAsync(int id);
    }
}
