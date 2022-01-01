using LogicCore.Common;
using LogicCore.Tasking.Scheduler;
using MentolVKS.Common.TypeExtensions;
using MentolVKS.Model.BaseModel;
using MentolVKS.Model.Enums;
using MentolVKS.Model.NotifyModel;
using MentolVKS.Service.Contract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentolVKS.Service
{
    public partial class Service : IService
    {
        private async Task AddRecordingVksUserEvent(NotifyOperation operation, RecordingVksUsers rec)
        {
            RecordingVksUserNotifyModel model = new RecordingVksUserNotifyModel();
            try
            {
                var aspNetUser = await UnitOfWork.AspNetUserRepository.GetByIdAsync(rec.UserId);
                var recording = await UnitOfWork.RecordingRepository.GetByIdAsync(rec.RecordingId);
                var booking = await UnitOfWork.BookingRepository.GetByIdAsync(recording.BookingId);

                model.UserEmail = aspNetUser.Email;
                model.UserName = aspNetUser.UserFullName;
                model.RecordingId = recording.Id;
                model.RecordingUrl = recording.Url;
                model.RecordingVksUsersIsPlay = rec.IsPlay;
                model.RecordingVksUsersIsDownload = rec.IsDownload;
                model.RecordingVksUsersDescription = rec.Description;
                model.BookingName = booking.Name;
            }
            catch { }

            var ntfEvent = new NtfEvents();
            ntfEvent.UploadDate = System.DateTime.Now;
            ntfEvent.WebPageName = NotifyPageName.MMS_BOOKING_RECORDS.GetDisplayName();
            ntfEvent.OperationInfo = operation.GetDisplayName();
            ntfEvent.Param1 = rec.Id.ToString();
            ntfEvent.Param2 = JsonConvert.SerializeObject(model, Formatting.Indented);

            var service = await UnitOfWork.ServicesRepository.GetByNameAsync(ServiceName.mentolbooking.GetDisplayName());
            ntfEvent.ServiceId = service?.Id;

            await UnitOfWork.NtfEventsRepository.AddAsync(ntfEvent);
        }

        private async Task AddRecordingEvent(NotifyOperation operation, Recording rec)
        {
            RecordingNotifyModel model = new RecordingNotifyModel();
            var booking = await UnitOfWork.BookingRepository.GetByIdAsync(rec.BookingId);

            model.BookingName = booking.Name;
            model.RecordingUrl = rec.Url;

            if (booking.OwnerId != null)
            {
                var user = await UnitOfWork.VksUserRepository.GetByIdAsync(booking.OwnerId);
                model.VksUserEmail = user.Email;
            }

            var ntfEvent = new NtfEvents();
            ntfEvent.UploadDate = System.DateTime.Now;
            ntfEvent.WebPageName = NotifyPageName.MMS_BOOKING_RECORDS.GetDisplayName();
            ntfEvent.OperationInfo = operation.GetDisplayName();
            ntfEvent.Param1 = rec.Id.ToString();
            ntfEvent.Param2 = JsonConvert.SerializeObject(model, Formatting.Indented);

            var service = await UnitOfWork.ServicesRepository.GetByNameAsync(ServiceName.mentolbooking.GetDisplayName());
            ntfEvent.ServiceId = service?.Id;

            await UnitOfWork.NtfEventsRepository.AddAsync(ntfEvent);
        }
        private async Task AddEventsBooking(NotifyOperation operation, Booking booking)
        {
            NotifyBookingModel model = new NotifyBookingModel();

            if (booking != null)
            {
                if (booking.OwnerId.HasValue)
                {
                    var vksUser = await UnitOfWork.VksUserRepository.GetByIdAsync(booking.OwnerId);
                    if (vksUser != null)
                    {
                        model.VksUserName = vksUser.Name;
                        model.VksUserEmail = vksUser.Email;
                        model.VksUsersEmails.Add(vksUser.Email);
                    }
                }

                var tm = await UnitOfWork.TimeZoneRepository.GetByIdAsync(booking.Timezone.Value);

                DateTime dtStart = booking.DateStart.Value;

                try
                {
                    var condition = SchedulerFormatter.Default.Parse(booking.Schedule);
                    condition.DateStart = new Date(dtStart.Year, dtStart.Month, dtStart.Day);
                    condition.TimeStart = new TimeSpan(dtStart.Hour, dtStart.Minute, dtStart.Second);

                    dtStart = condition.GetNextTime(DateTime.Now, null);
                }
                catch { }

                model.BookingName = booking.Name;
                model.BookingDateStart = dtStart.ToString("dd.MM.yyyy HH:mm:ss") + Offset(tm.OffsetMinute) + TimeSpan.FromMinutes(tm.OffsetMinute).ToString(@"hh\:mm");
                model.BookingDuration = booking.Duration.HasValue ? booking.Duration.ToString() : "";
                model.BookingPinCode = booking.PinCode;
                model.BookingOpenConferenceBefore = booking.OpenConferenceBefore.HasValue ? booking.OpenConferenceBefore.ToString() : "";
                model.BookingLocation = booking.Location;
                model.Uid = booking.Uid;            
                model.BookingTimeZoneId = tm != null ? tm.StandartId : "";
                model.BookingTimeZoneName = tm != null ? tm.Name : "";
                model.OperationName = operation.GetDisplayName();

                var space = await UnitOfWork.SpaceRepository.GetByIdAsync(booking.SpaceId);
                model.SpaceUri = space != null ? space.Uri : "";
                model.SpaceCallId = space != null ? space.CallId : "";
                model.SpacePasswordGuest = space != null ? space.PasswordGuest : "";

                foreach (var item in booking.LinkBookingToParticipants)
                {
                    var user = await UnitOfWork.VksUserRepository.GetByIdAsync(item.VksParticipantId);

                    if (user != null && !model.VksUsersEmails.Contains(user.Email))
                    {
                        model.VksUsersEmails.Add(user.Email);
                    }
                }

                foreach (var item in booking.LinkBookingTovksUsersOthers)
                {
                    var user = await UnitOfWork.VksUsersOtherRepository.GetByIdAsync(item.VksUsersOtherId);

                    if (user != null && !model.VksUsersEmails.Contains(user.Email))
                    {
                        model.VksUsersEmails.Add(user.Email);
                    }
                }
                model.VksUsersEmails = model.VksUsersEmails.Where(c => !string.IsNullOrEmpty(c)).ToList();
            }

            var ntfEvent = new NtfEvents();
            ntfEvent.UploadDate = System.DateTime.Now;
            ntfEvent.WebPageName = NotifyPageName.MMS_BOOKING_ROOMS.GetDisplayName();
            ntfEvent.OperationInfo = operation.GetDisplayName();
            ntfEvent.Param1 = booking.Id.ToString();
            ntfEvent.Param2 = JsonConvert.SerializeObject(model, Formatting.Indented);

            var service = await UnitOfWork.ServicesRepository.GetByNameAsync(ServiceName.mentolbooking.GetDisplayName());
            ntfEvent.ServiceId = service?.Id;

            await UnitOfWork.NtfEventsRepository.AddAsync(ntfEvent);
        }

        private async Task AddUpdateEventsBooking(Booking booking, List<string> update, List<string> delete, List<string> add)
        {
            NotifyBookingModel model = new NotifyBookingModel();

            if (booking != null)
            {
                if (booking.OwnerId.HasValue)
                {
                    var vksUser = await UnitOfWork.VksUserRepository.GetByIdAsync(booking.OwnerId);
                    if (vksUser != null)
                    {
                        model.VksUserName = vksUser.Name;
                        update.Add(vksUser.Email);
                    }
                }

                var tm = await UnitOfWork.TimeZoneRepository.GetByIdAsync(booking.Timezone.Value);

                DateTime dtStart = booking.DateStart.Value;

                try
                {
                    var condition = SchedulerFormatter.Default.Parse(booking.Schedule);
                    condition.DateStart = new Date(dtStart.Year, dtStart.Month, dtStart.Day);
                    condition.TimeStart = new TimeSpan(dtStart.Hour, dtStart.Minute, dtStart.Second);

                    dtStart = condition.GetNextTime(DateTime.Now, null);
                }
                catch { }

                model.BookingName = booking.Name;
                model.BookingDateStart = dtStart.ToString("dd.MM.yyyy HH:mm:ss") + Offset(tm.OffsetMinute) + TimeSpan.FromMinutes(tm.OffsetMinute).ToString(@"hh\:mm");
                model.BookingDuration = booking.Duration.HasValue ? booking.Duration.ToString() : "";
                model.BookingPinCode = booking.PinCode;
                model.BookingOpenConferenceBefore = booking.OpenConferenceBefore.HasValue ? booking.OpenConferenceBefore.ToString() : "";
                model.BookingLocation = booking.Location;
                model.Uid = booking.Uid;
                model.BookingTimeZoneId = tm != null ? tm.StandartId : "";
                model.BookingTimeZoneName = tm != null ? tm.Name : "";


                var space = await UnitOfWork.SpaceRepository.GetByIdAsync(booking.SpaceId);
                model.SpaceUri = space != null ? space.Uri : "";
                model.SpaceCallId = space != null ? space.CallId : "";
                model.SpacePasswordGuest = space != null ? space.PasswordGuest : "";
            }

            if (update.Count > 0)
            {
                model.VksUsersEmails = new List<string>();
                model.OperationName = NotifyOperation.BOOKING_EDIT.GetDisplayName();
                model.VksUsersEmails.AddRange(update.Where(c => !String.IsNullOrEmpty(c)).ToList());
                model.VksUsersEmails.AddRange(add.Where(c => !String.IsNullOrEmpty(c)).ToList());
                var ntfEvent = new NtfEvents();
                ntfEvent.UploadDate = System.DateTime.Now;
                ntfEvent.WebPageName = NotifyPageName.MMS_BOOKING_ROOMS.GetDisplayName();
                ntfEvent.OperationInfo = NotifyOperation.BOOKING_EDIT.GetDisplayName();
                ntfEvent.Param1 = booking.Id.ToString();
                ntfEvent.Param2 = JsonConvert.SerializeObject(model, Formatting.Indented);

                var service = await UnitOfWork.ServicesRepository.GetByNameAsync(ServiceName.mentolbooking.GetDisplayName());
                ntfEvent.ServiceId = service?.Id;

                await UnitOfWork.NtfEventsRepository.AddAsync(ntfEvent);
            }

            if (delete.Count > 0)
            {
                model.VksUsersEmails = new List<string>();
                model.OperationName = NotifyOperation.BOOKING_DELETE.GetDisplayName();
                model.VksUsersEmails.AddRange(delete.Where(c => !String.IsNullOrEmpty(c)).ToList());
                var ntfEvent = new NtfEvents();
                ntfEvent.UploadDate = System.DateTime.Now;
                ntfEvent.WebPageName = NotifyPageName.MMS_BOOKING_ROOMS.GetDisplayName();
                ntfEvent.OperationInfo = NotifyOperation.BOOKING_DELETE.GetDisplayName();
                ntfEvent.Param1 = booking.Id.ToString();
                ntfEvent.Param2 = JsonConvert.SerializeObject(model, Formatting.Indented);

                var service = await UnitOfWork.ServicesRepository.GetByNameAsync(ServiceName.mentolbooking.GetDisplayName());
                ntfEvent.ServiceId = service?.Id;

                await UnitOfWork.NtfEventsRepository.AddAsync(ntfEvent);
            }

            if (add.Count > 0)
            {
                model.VksUsersEmails = new List<string>();
                model.OperationName = NotifyOperation.BOOKING_ADD.GetDisplayName();
                model.VksUsersEmails.AddRange(add.Where(c => !String.IsNullOrEmpty(c)).ToList());
                var ntfEvent = new NtfEvents();
                ntfEvent.UploadDate = System.DateTime.Now;
                ntfEvent.WebPageName = NotifyPageName.MMS_BOOKING_ROOMS.GetDisplayName();
                ntfEvent.OperationInfo = NotifyOperation.BOOKING_ADD.GetDisplayName();
                ntfEvent.Param1 = booking.Id.ToString();
                ntfEvent.Param2 = JsonConvert.SerializeObject(model, Formatting.Indented);

                var service = await UnitOfWork.ServicesRepository.GetByNameAsync(ServiceName.mentolbooking.GetDisplayName());
                ntfEvent.ServiceId = service?.Id;

                await UnitOfWork.NtfEventsRepository.AddAsync(ntfEvent);
            }


        }
        private string Offset(int value)
        {
            return value >= 0 ? "+" : "-";
        }
    }
}