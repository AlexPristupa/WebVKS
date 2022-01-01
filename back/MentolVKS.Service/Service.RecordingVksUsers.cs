using MentolVKS.Model.BaseModel;
using MentolVKS.Model.Validation;
using MentolVKS.Service.Contract;
using System.Threading.Tasks;

namespace MentolVKS.Service
{
    public partial class Service : IService
    {
        /// <inheritdoc />
        public async Task<RecordingVksUsers> AddRecordingVksUsersAsync(RecordingVksUsers item)
        {
            if (!item.IsPlay && !item.IsDownload)
            {
                throw new ValidationErrors(new GeneralError(Localizer["You must assign rights! 'View' or 'Download' rights must be assigned."]));
            }

            var check = await UnitOfWork.RecordingVksUsersRepository.GetByRecordingIdAndUserIdAsync(item.RecordingId, item.UserId);
            if (check != null)
            {
                throw new ValidationErrors(new GeneralError(Localizer["The user has already been assigned rights."]));
            }

            var result = await UnitOfWork.RecordingVksUsersRepository.AddAsync(item);

            var record = await UnitOfWork.RecordingVksUsersRepository.GetByIdAsync(result.Id);
            var rights = (record.IsPlay ? "View" : "") + (record.IsDownload ? (record.IsPlay ? " and Loading" : "Loading") : "");
            
            await AddRecordingVksUserEvent(Model.Enums.NotifyOperation.RECORDINGVKSUSERS_ADD, record);
            await AddSuccessLog(
                Model.Enums.ProductType.MMS, Model.Enums.LogTypes.RECORDS, Localizer["Add rights"]
                , Localizer["For the user \"{0}\", added rights: {1} on record {2} for booking \"{3}\"."
                , record.User.UserFullName + " ("+record.User.Email+")", Localizer[rights], record.Recording.Url + " (" + record.Recording.Id + ")", record.Recording.Booking.Name]
                , result.Id
                );

            return result;
        }

        /// <inheritdoc />
        public async Task<RecordingVksUsers> UpdateRecordingVksUsersAsync(RecordingVksUsers item)
        {
            var oldRecord = await UnitOfWork.RecordingVksUsersRepository.GetByIdAsync(item.Id);

            if (oldRecord == null)
            {
                throw new ValidationErrors(new GeneralError(Localizer["Record with id {0} not found!", item.Id]));
            }

            if (!item.IsPlay && !item.IsDownload)
            {
                throw new ValidationErrors(new GeneralError(Localizer["You must assign rights! 'View' or 'Download' rights must be assigned."]));
            }
            var result = await UnitOfWork.RecordingVksUsersRepository.SaveAsync(item);
            
            var newRecord = await UnitOfWork.RecordingVksUsersRepository.GetByIdAsync(result.Id);
            var isPlayLog = oldRecord.IsPlay != newRecord.IsPlay ? (newRecord.IsPlay ? "added rights:" : "removed rights:") : "";
            var isDownloadLog = oldRecord.IsDownload != newRecord.IsDownload ? (newRecord.IsDownload ? "added rights:" : "removed rights:") : "";

            await AddRecordingVksUserEvent(Model.Enums.NotifyOperation.RECORDINGVKSUSERS_EDIT, result);

            if (!string.IsNullOrEmpty(isPlayLog))
            {
                await AddSuccessLog(
                    Model.Enums.ProductType.MMS, Model.Enums.LogTypes.RECORDS, Localizer["Edit rights"]
                    , Localizer["For the user \"{0}\", {1} {2} on record {3} for booking \"{4}\"."
                    , newRecord.User.UserFullName + " (" + newRecord.User.Email + ")", Localizer[isPlayLog], Localizer["View"]
                    , newRecord.Recording.Url + " (" + newRecord.Recording.Id + ")", newRecord.Recording.Booking.Name]
                    , result.Id
                    );
            }

            if (!string.IsNullOrEmpty(isDownloadLog))
            { 
                await AddSuccessLog(
                Model.Enums.ProductType.MMS, Model.Enums.LogTypes.RECORDS, Localizer["Edit rights"]
                , Localizer["For the user \"{0}\", {1} {2} on record {3} for booking \"{4}\"."
                , newRecord.User.UserFullName + " (" + newRecord.User.Email + ")", Localizer[isDownloadLog], Localizer["Loading"]
                , newRecord.Recording.Url + " (" + newRecord.Recording.Id + ")", newRecord.Recording.Booking.Name]
                , result.Id
                );
            }

            return result;
        }

        /// <inheritdoc />
        public async Task DeleteRecordingVksUsersAsync(int id)
        {
            var check = await UnitOfWork.RecordingVksUsersRepository.GetByIdAsync(id);

            if (check == null)
            {
                throw new ValidationErrors(new GeneralError(Localizer["Record with id {0} not found!", id]));
            }

            await UnitOfWork.RecordingVksUsersRepository.DeleteAsync(check);
            check.IsDownload = check.IsPlay = false;
            await AddRecordingVksUserEvent(Model.Enums.NotifyOperation.RECORDINGVKSUSERS_DELETE, check);

            await AddSuccessLog(Model.Enums.ProductType.MMS, Model.Enums.LogTypes.RECORDS, Localizer["Delete rights"]
                , Localizer["For the user \"{0}\", removed rights: View and Loading on record {1} for booking \"{2}\"."
                , check.User.UserFullName + " (" + check.User.Email + ")", check.Recording.Url + " (" + check.Recording.Id + ")", check.Recording.Booking.Name]
                , check.Id);
        }
    }
}