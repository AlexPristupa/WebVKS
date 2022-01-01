using MentolVKS.Model.BaseModel;
using MentolVKS.Model.Validation;
using MentolVKS.Service.Contract;
using System.IO;
using System.Threading.Tasks;

namespace MentolVKS.Service
{
    public partial class Service : IService
    {
        /// <inheritdoc />
        public async Task DeleteRecordingAsync(int id)
        {
            var check = await GetRecordingByIdAsync(id);

            if (check == null)
            {
                throw new ValidationErrors(new GeneralError(Localizer["Record with id {0} not found!", id]));
            }

            if(await UnitOfWork.RecordingVksUsersRepository.ChekcByRecrodingIdAsync(check.Id))
                throw new ValidationErrors(new GeneralError(Localizer["Запись для конференции \"{0}\" не может быть удалена, для нее есть назначенные права!", check.Booking.Name]));

            await UnitOfWork.RecordingRepository.DeleteAsync(check);
            if (File.Exists(check.Url)) File.Delete(check.Url);
            await AddRecordingEvent(Model.Enums.NotifyOperation.RECORDING_DELETE, check);
            await AddSuccessLog(Model.Enums.ProductType.MMS, Model.Enums.LogTypes.RECORDS, Localizer["delete"]
                , Localizer["Delete record {0} ({1}) from reservation \"{2}\""
                , check.Url, check.Id, check.Booking?.Name], check.Id);
        }

        public async Task<Recording> GetRecordingByIdAsync(int id)
        {
            var nfsServer = await NfsServersGetFirstAsync();
            var recording = await UnitOfWork.RecordingRepository.GetByIdAsync(id);
            if (recording != null)
            {
                recording.Url = nfsServer?.Mount + recording.Url;
            }

            return recording;
        }
    }
}