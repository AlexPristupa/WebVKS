using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using LogicCore.Tasking.Scheduler;
using LogicCore.Tasking.Scheduler.Conditions;
using MentolVKS.Model.BaseModel;
using MentolVKS.Model.Report;
using MentolVKS.Model.Validation;
using MentolVKS.Service.Contract;
using TimeZoneConverter;

namespace MentolVKS.Service
{
    public partial class Service : IService
    {
        /// <inheritdoc />
        public async Task<List<BookingView>> BookingViewGetBySpaceIdAsync(int spaceId)
        {
            return await UnitOfWork.BookingViewRepository.GetBySpaceIdAsync(spaceId);
        }

        /// <inheritdoc />
        public async Task<Booking> GetBookingByIdAsync(int id)
        {
            var result = await UnitOfWork.BookingRepository.GetByIdAsync(id);

            result.LinkBookingToParticipants = await UnitOfWork.LinkBookingToParticipantRepository.AllByBookingIdAsync(result.Id);
            result.Owner = await UnitOfWork.VksUserRepository.GetByIdAsync(result.OwnerId);
            result.LinkBookingTovksUsersOthers = await UnitOfWork.LinkBookingTovksUsersOthersRepository.AllByBookingAsync(id);

            return result;
        }

        /// <inheritdoc />
        public async Task<Booking> AddBookingAsync(Booking item, bool outlook = false)
        {
            if (item.IsUsePin.Value && string.IsNullOrEmpty(item.PinCode))
            {
                item.PinCode = RandomNumberGenerator.GetInt32(1000, 9999).ToString();
            }

            var space = await UnitOfWork.SpaceRepository.GetByIdAsync(item.SpaceId);
            if (space == null)
            {
                throw new ValidationErrors(new GeneralError(Localizer["The room you own is missing. Contact your system administrator."]));
            }

            await CheckAddLinkToUsersOtherAsync(item);
            await CheckAddLinkToPrincipalAsync(item);
            if(string.IsNullOrEmpty(item.Uid))
                item.Uid = UidGenerator();

            var result = await UnitOfWork.BookingRepository.AddAsync(item);
            result.Owner = await UnitOfWork.VksUserRepository.GetByIdAsync(result.OwnerId);

            var spaceName = await SpaceName(result.SpaceId);
            await AddSuccessLog(Model.Enums.ProductType.MMS, Model.Enums.LogTypes.BOOKING, Localizer["add"], Localizer["Added reservation \"{0}\" for the room \"{1}\"", result.Name, spaceName], result.Id);

            if (result.LinkBookingToParticipants.Count > 0)
            {
                foreach (var buf in result.LinkBookingToParticipants)
                {
                    var user = await UnitOfWork.VksUserRepository.GetByIdAsync(buf.VksParticipantId);
                    await AddSuccessLog(Model.Enums.ProductType.MMS, Model.Enums.LogTypes.BOOKING, Localizer["add"], Localizer["Reservation \"{0}\" for the room \"{1}\" add user \"{2}\"", result.Name, spaceName, user.Name], result.Id);

                    buf.VksParticipant = user;
                }
            }
            else
            {
                result.LinkBookingToParticipants = await UnitOfWork.LinkBookingToParticipantRepository.AllByBookingIdAsync(result.Id);
            }

            if(outlook==false)
                if (result.IsSendNotification.Value)
                    await AddEventsBooking(Model.Enums.NotifyOperation.BOOKING_ADD, result);

            return result;
        }

        /// <inheritdoc />
        public async Task DeleteBookingByUuid(string uuid)
        {
            var booking = await UnitOfWork.BookingRepository.GetByUidAsync(uuid);

            if (booking != null)
                await UnitOfWork.BookingRepository.DeleteAsync(booking);
        }
        /// <inheritdoc />
        public async Task<BookingOutlook> AddOutlookBookingAsync(int userId, 
            string name, 
            string location, 
            DateTime dateStart, 
            int timeZoneMinute, 
            int connectionTypeId, 
            int openConferenceBefore, 
            string emails, 
            string ics, 
            string uuid, 
            int duration, 
            string organizer, 
            DateTime dateEnd,
            string timezonename)
        {
            BookingOutlook result = new BookingOutlook();
            var defOutloock = (await UnitOfWork.OutlookBookingDefaultRepository.AllAsync()).FirstOrDefault();
            var vksUser = await UnitOfWork.VksUserRepository.AllAsync();
            var vksOther = await UnitOfWork.VksUsersOtherRepository.AllAsync();

            var currentTimeZone = await UnitOfWork.TimeZoneRepository.GetByStandartId(timezonename);
            if (currentTimeZone == null)
            {
                try
                {
                    var tmz = TZConvert.GetTimeZoneInfo(timezonename);

                    if (tmz != null)
                    {
                        currentTimeZone = await UnitOfWork.TimeZoneRepository.GetByStandartId(tmz.Id);
                    }
                }
                catch  { }
            }

            if (currentTimeZone == null)
            {
                currentTimeZone = await UnitOfWork.TimeZoneRepository.GetTimeZoneByOffset(timeZoneMinute);
            }

            var spaceId = await UnitOfWork.SpaceRepository.GetFirstSpaceAsync();
            var aspNetUser = await UnitOfWork.AspNetUserRepository.GetByIdAsync(userId);
            
            
            var vksCurrentUser = vksUser.FirstOrDefault(c => c.JID.ToUpper() == UserName.GetUserName().ToUpper());
            
            if (vksCurrentUser == null)
            {
                vksCurrentUser = await UnitOfWork.VksUserRepository.AddAsync(new VksUser {
                   JID=aspNetUser.UserName,
                   Name = aspNetUser.UserFullName,  
                   Email = organizer
                });
            }

            Booking model = null;

            model = await UnitOfWork.BookingRepository.GetByUidAsync(uuid);

            if (model == null)
            {
                model = new Booking
                {
                    PinCode = string.Empty,
                    Duration = duration==0 ? defOutloock.Duration : duration,
                    IsUsePin = defOutloock.IsUsePin,
                    Schedule = defOutloock.Schedule,
                    SpaceId = spaceId.Id,
                    AttemptsCount = defOutloock.AtTemptsCount,
                    Delay = defOutloock.Delay,
                    IsSendNotification = defOutloock.IsSendNotification,
                    IsSyncToExchange = defOutloock.IsSyncToExchange,
                    IsNeverUsePin = defOutloock.IsNeverUsePin,
                    RepeatCount = defOutloock.RepeatCount,
                    PinPoliticsId = defOutloock.PinPoliticsId,
                    PinSchedule = defOutloock.PinSchedule,
                    PinDateStart = DateTime.UtcNow,
                    ScheduleTab = defOutloock.ScheduleTab,
                    PinScheduleTab = defOutloock.PinScheduleTab,
                    TypeId = defOutloock.TypeId,
                    OwnerId = vksCurrentUser.Id,
                    Uid = uuid,
                    LinkBookingToParticipants = new List<LinkBookingToParticipant>()                    
                };
            }

            model.DateStart = dateStart;
            model.Duration = duration == 0 ? defOutloock.Duration : duration;
            model.DateEnd = dateEnd.Year == 1 ? dateStart.AddMinutes(model.Duration.Value) : dateEnd;
            model.Name = name;
            model.Location = location;
            model.Timezone = currentTimeZone.Id;
            model.ConnectionTypeId = connectionTypeId;
            model.OpenConferenceBefore = openConferenceBefore;

            try
            {
                var emailList = emails.Split(";")
                    .Distinct()
                    .Where(c => !String.IsNullOrEmpty(c))
                    .ToList()
                    .Where(c => c.ToLower() != organizer.ToLower())
                    .ToList();
                
                foreach (var item in emailList)
                {
                    var el = vksUser.FirstOrDefault(c => c.Email.ToLower() == item.ToLower());
                    if (el != null)
                    {
                        model.LinkBookingToParticipants.Add(new LinkBookingToParticipant
                        {
                            BookingId = model.Id,
                            Id = 0,
                            VksParticipantId = el.Id,
                            VksParticipant = el
                        });
                    }
                    else
                    {
                        var el2 = vksOther.FirstOrDefault(c => c.Email.ToLower() == item.ToLower());
                        if (el2 != null)
                        {
                            model.LinkBookingTovksUsersOthers.Add(new LinkBookingTovksUsersOther
                            {
                                BookingId = model.Id,
                                Id = 0,
                                VksUsersOtherId = el2.Id,
                                VksUsersOther = el2
                            });
                        }
                        else
                        {
                            try
                            {
                                var bufOther = await UnitOfWork.VksUsersOtherRepository.AddAsync(new VksUsersOther
                                {
                                    Email = item,
                                    Name = String.Empty,
                                    Uri = string.Empty
                                });
                                model.LinkBookingTovksUsersOthers.Add(new LinkBookingTovksUsersOther
                                {
                                    BookingId = model.Id,
                                    Id = 0,
                                    VksUsersOtherId = bufOther.Id,
                                    VksUsersOther = bufOther
                                });
                            }
                            catch  { }
                        }
                    }
                }
                if (model.Id == 0)
                    model = await AddBookingAsync(model, true);
                else
                    model = await UpdateBookingAsync(model, true);

                result.Template = defOutloock.RtfFileTemplate;
                result.Template = result.Template.Replace("\\{id\\}", model.Id.ToString());
                result.Template = result.Template.Replace("\\{space_uri\\}", spaceId.Uri);
                result.Template = result.Template.Replace("\\{booking_pincode\\}", model.PinCode);
                result.Template = result.Template.Replace("\\{booking_openconferencebefore\\}", model.OpenConferenceBefore.ToString());
                result.Template = result.Template.Replace("\\{space_uri\\}", spaceId.Uri);
                result.Id = model.Id;
            }
            catch (Exception ex)
            {
                throw new ValidationErrors(new GeneralError(ex.Message));
            }

            return result;
        }

        private async Task<VksUsersOther> VksUsersOtherCreateOrUpdate(VksUsersOther item)
        {
            if (item.Id == 0)
            {
                if (string.IsNullOrEmpty(item.Uri))
                {
                    var otherUser = await UnitOfWork.VksUsersOtherRepository.GetByEmptyUriAndEmailAsync(item.Email);

                    if (otherUser != null)
                    {
                        if (otherUser.Name != item.Name)
                        {
                            otherUser.Name = item.Name;
                            return await UnitOfWork.VksUsersOtherRepository.SaveAsync(otherUser);
                        }

                        return otherUser;
                    }
                    else
                    {
                        return await UnitOfWork.VksUsersOtherRepository.AddAsync(new VksUsersOther
                        {
                            Email = item.Email ?? String.Empty,
                            Uri = item.Uri ?? String.Empty,
                            Name = item.Name ?? String.Empty
                        });
                    }
                }
                else
                {
                    var otherUser = await UnitOfWork.VksUsersOtherRepository.GetByUriAsync(item.Uri);

                    if (otherUser != null)
                    {
                        throw new ValidationErrors(new GeneralError($"URI участника \"{item.Uri}\" уже существует в системе. Выберите существующий или укажите уникальное значение для URI участника."));
                    }

                    return await UnitOfWork.VksUsersOtherRepository.AddAsync(new VksUsersOther
                    {
                        Email = item.Email ?? String.Empty,
                        Uri = item.Uri,
                        Name =item.Name ?? String.Empty
                    });
                }
            }
            else
            {
                var other = await UnitOfWork.VksUsersOtherRepository.GetByIdAsync(item.Id);
                                
                if (other.Uri != item.Uri)
                {
                    if (string.IsNullOrEmpty(item.Uri))
                    {
                        var otherUser = await UnitOfWork.VksUsersOtherRepository.GetByEmptyUriAndEmailAsync(item.Email);
                        if (otherUser != null)
                        {
                            throw new ValidationErrors(new GeneralError($"URI участника \"{item.Uri}\" уже существует в системе. Выберите существующий или укажите уникальное значение для URI участника."));
                        }

                        other.Email = item.Email;
                        other.Uri = String.Empty;
                        other.Name = item.Name;

                        return await UnitOfWork.VksUsersOtherRepository.SaveAsync(other);
                    }
                    else
                    {
                        var otherUser = await UnitOfWork.VksUsersOtherRepository.GetByUriAndNotId(item.Uri, other.Id);
                        
                        if (otherUser != null)
                        {
                            throw new ValidationErrors(new GeneralError($"URI участника \"{item.Uri}\" уже существует в системе. Выберите существующий или укажите уникальное значение для URI участника."));
                        }
                        
                        other.Name = item.Name;
                        other.Uri = item.Uri;
                        other.Email = item.Email;

                        return await UnitOfWork.VksUsersOtherRepository.SaveAsync(other);
                    }                                    
                }

                if(other.Name!=item.Name || other.Email != item.Email)
                {
                    other.Name = item.Name;
                    other.Email = item.Email;

                    return await UnitOfWork.VksUsersOtherRepository.SaveAsync(other);
                }

                return other;
            }
        }
        /// <inheritdoc />
        public async Task<Booking> UpdateBookingAsync(Booking item, bool outlook = false)
        {
            var newLinks = item.LinkBookingToParticipants;
            var newUserIds = item.LinkBookingToParticipants.Select(u => u.VksParticipantId);
            var newOtherLinks = item.LinkBookingTovksUsersOthers;
            var newOtherUserIds = new List<int>();

            List<string> RemoveEmails = new List<string>();
            List<string> AddEmails = new List<string>();
            List<string> UpdateEmails = new List<string>();

            item.LinkBookingToParticipants = null;
            item.LinkBookingTovksUsersOthers = null;

            #region Проверка и создание other Users
            if (newOtherLinks != null)
            {
                for (var i = 0; i < newOtherLinks.Count; i++)
                {
                    newOtherLinks[i].VksUsersOther = await VksUsersOtherCreateOrUpdate(newOtherLinks[i].VksUsersOther);
                    newOtherLinks[i].VksUsersOtherId = newOtherLinks[i].VksUsersOther.Id;
                }
                newOtherUserIds = newOtherLinks.Select(u => u.VksUsersOtherId != null ? u.VksUsersOtherId.Value : 0).ToList();
            }
            #endregion

            var check = await UnitOfWork.BookingRepository.GetByIdAsync(item.Id);

            if (check == null)
            {
                item.Uid = UidGenerator();
            }
            else
            {
                item.Uid = String.IsNullOrEmpty(check.Uid) ? UidGenerator() : check.Uid;
            }

            //await CheckUpdateLinkToUsersOtherAsync(item);

            var result = await UnitOfWork.BookingRepository.SaveAsync(item);

            var spaceName = await SpaceName(result.SpaceId);

            Type t = result.GetType();
            foreach (PropertyInfo info in t.GetProperties())
            {
                var oldValue = check.GetType().GetProperties().FirstOrDefault(c => c.Name == info.Name).GetValue(check);
                var newValue = info.GetValue(result);
                oldValue = oldValue != null ? oldValue.ToString() : string.Empty;
                newValue = newValue != null ? newValue.ToString() : string.Empty;

                if (!oldValue.Equals(newValue))
                {
                    if (info.Name != "LinkBookingToParticipants")
                        await AddSuccessLog(Model.Enums.ProductType.MMS, Model.Enums.LogTypes.BOOKING, Localizer["edit"], Localizer["Edit reservation \"{0}\" for the room \"{1}\". Changed value for \"{2}\" from \"{3}\" to \"{4}\"", item.Name, spaceName, info.Name, oldValue, newValue], item.Id);
                }
            }

            var oldLink = await UnitOfWork.LinkBookingToParticipantRepository.AllByBookingIdAsync(result.Id);
            var oldUserIds = oldLink.Select(c => c.VksParticipantId).ToList();
            var oldOtherLink = await UnitOfWork.LinkBookingTovksUsersOthersRepository.AllByBookingAsync(result.Id);
            var oldOtherUserIds = oldOtherLink.Select(c => c.VksUsersOtherId.Value).ToList();

            var removeIds = oldUserIds.Except(newUserIds.Where(c=>c !=0).ToList()).ToList();
            var createIds = newUserIds.Except(oldUserIds).ToList();
            var updateIds = newUserIds.Except(createIds.Where(c=>c!=0).ToList()).ToList();

            var removeOtherIds = oldOtherUserIds.Except(newOtherUserIds).ToList();
            var createOtherIds = newOtherUserIds.Except(oldOtherUserIds).ToList();
            var updateOtherId = newOtherUserIds.Except(createIds).ToList();

            foreach (var buf in removeIds)
            {
                var remove = oldLink.FirstOrDefault(c => c.VksParticipantId == buf);
                var user = await UnitOfWork.VksUserRepository.GetByIdAsync(buf);
                await UnitOfWork.LinkBookingToParticipantRepository.DeleteAsync(remove);
                await AddSuccessLog(Model.Enums.ProductType.MMS, Model.Enums.LogTypes.BOOKING, Localizer["edit"], Localizer["Reservation \"{0}\" for the room \"{1}\" delete user \"{2}\"", item.Name, spaceName, user.Name], item.Id);
                RemoveEmails.Add(user.Email);
            }

            foreach (var buf in removeOtherIds)
            {
                var remove = oldOtherLink.FirstOrDefault(c => c.VksUsersOtherId == buf);
                var user = await UnitOfWork.VksUsersOtherRepository.GetByIdAsync(buf);
                await UnitOfWork.LinkBookingTovksUsersOthersRepository.DeleteAsync(remove);
                await AddSuccessLog(Model.Enums.ProductType.MMS, Model.Enums.LogTypes.BOOKING, Localizer["edit"], Localizer["Reservation \"{0}\" for the room \"{1}\" delete user \"{2}\"", item.Name, spaceName, user.Name], item.Id);
                RemoveEmails.Add(user.Email);
            }

            foreach (var buf in createIds)
            {               
                var user = await UnitOfWork.VksUserRepository.GetByIdAsync(buf);

                var newLink = newLinks.FirstOrDefault(c => c.VksParticipantId == buf);

                await UnitOfWork.LinkBookingToParticipantRepository.AddAsync(new LinkBookingToParticipant
                {
                    Id = 0,
                    BookingId = result.Id,
                    VksParticipantId = buf,
                    CallLegProfileGuid = newLink.CallLegProfileGuid,
                    Uri = newLink.Uri
                });
                await AddSuccessLog(Model.Enums.ProductType.MMS, Model.Enums.LogTypes.BOOKING, Localizer["edit"], Localizer["Reservation \"{0}\" for the room \"{1}\" add user \"{2}\"", item.Name, spaceName, user.Name], item.Id);
                AddEmails.Add(user.Email);
            }

            foreach (var buf in createOtherIds)
            {
                var user = await UnitOfWork.VksUsersOtherRepository.GetByIdAsync(buf);

                var newLink = newOtherLinks.FirstOrDefault(c => c.VksUsersOtherId == buf);

                await UnitOfWork.LinkBookingTovksUsersOthersRepository.AddAsync(new LinkBookingTovksUsersOther
                {
                    Id = 0,
                    BookingId = result.Id,
                    VksUsersOtherId = buf
                });
                await AddSuccessLog(Model.Enums.ProductType.MMS, Model.Enums.LogTypes.BOOKING, Localizer["edit"], Localizer["Reservation \"{0}\" for the room \"{1}\" add user \"{2}\"", item.Name, spaceName, user.Name], item.Id);
                AddEmails.Add(user.Email);
            }

            var saveLinks = new List<LinkBookingToParticipant>();

            foreach (var buf in updateIds)
            {
                var user = await UnitOfWork.VksUserRepository.GetByIdAsync(buf);
                var update = oldLink.FirstOrDefault(c => c.VksParticipantId == buf);
                var newLink = newLinks.FirstOrDefault(c => c.VksParticipantId == buf);

                if (update != null && newLink != null && (update.CallLegProfileGuid != newLink.CallLegProfileGuid || update.Uri != newLink.Uri))
                {
                    update.CallLegProfileGuid = newLink.CallLegProfileGuid;
                    update.Uri = newLink.Uri;
                    update.VksParticipant = null;

                    saveLinks.Add(update);
                }
                UpdateEmails.Add(user.Email);
            }

            foreach(var buf in updateOtherId)
            {
                var user = await UnitOfWork.VksUsersOtherRepository.GetByIdAsync(buf);
                UpdateEmails.Add(user.Email);
            }

            await UnitOfWork.LinkBookingToParticipantRepository.SaveRangeAsync(saveLinks);

            result.LinkBookingToParticipants = await UnitOfWork.LinkBookingToParticipantRepository.AllByBookingIdAsync(result.Id);
            result.LinkBookingTovksUsersOthers = await UnitOfWork.LinkBookingTovksUsersOthersRepository.AllByBookingAsync(result.Id);
            result.Owner = await UnitOfWork.VksUserRepository.GetByIdAsync(result.OwnerId);

            if(outlook==false)
                if (result.IsSendNotification.Value)
                    await AddUpdateEventsBooking(result, UpdateEmails, RemoveEmails, AddEmails);

            return result;
        }

        /// <inheritdoc />
        public async Task DeleteBookingAsync(int id)
        {
            var check = await UnitOfWork.BookingRepository.GetByIdAsync(id);

            if (check == null)
            {
                throw new ValidationErrors(new GeneralError(Localizer["Booking with id {0} not found!", id]));
            }

            var records = await UnitOfWork.RecordingRepository.CheckByBookingIdAsync(check.Id);
            if (records)
                throw new ValidationErrors(new GeneralError(Localizer["Бронирование \"{0}\" не может быть удалено, для него есть записи ВКС!", check.Name]));

            check.LinkBookingToParticipants = await UnitOfWork.LinkBookingToParticipantRepository.AllByBookingIdAsync(check.Id);
            check.LinkBookingTovksUsersOthers = await UnitOfWork.LinkBookingTovksUsersOthersRepository.AllByBookingAsync(id);

            await UnitOfWork.BookingRepository.DeleteAsync(check);
            await AddSuccessLog(Model.Enums.ProductType.MMS, Model.Enums.LogTypes.BOOKING, Localizer["delete"], Localizer["Delete reservation \"{0}\" for the room \"{1}\"", check.Name, await SpaceName(check.SpaceId)], check.Id);

            if (check.IsSendNotification.Value)
                await AddEventsBooking(Model.Enums.NotifyOperation.BOOKING_DELETE, check);
        }

        /// <summary>
        /// Проверка расписания на пересечение
        /// </summary>
        public async Task CheckShedule(int spaceId, string shedule, DateTime? startTime, int duration, int timeZone, DateTime? dateEnd, int repeatCount, int? bookingId)
        {
            var allTimeZone = await UnitOfWork.TimeZoneRepository.AllAsync();

            var currentTimeZone = allTimeZone.FirstOrDefault(c => c.Id == timeZone);

            if (currentTimeZone == null)
            {
                throw new ValidationErrors(new PropertyError("TimeZone", "Временная зона не найдена"));
            }

            if (!startTime.HasValue)
            {
                startTime = DateTime.Now;
            }

            startTime = startTime.Value.AddMinutes(currentTimeZone.OffsetMinute);

            List<SheduleList> currentSheduleList = new List<SheduleList>();

            // dateend и repeatcount - могут передаваться как dateend = NULL и repeatcount = 0, это означает, что бронирование не периодическое и выполняется один раз. При таких параметрах schedule будет пустой (schedule = "")
            if (string.IsNullOrEmpty(shedule) && dateEnd == null && repeatCount == 0)
            {
                currentSheduleList.Add(new SheduleList { StartTime = startTime.Value, EndTime = startTime.Value.AddMinutes(duration) });
            }

            // dateend и repeatcount - могут передаваться как dateend != NULL и repeatcount = 0, это означает, учитывается только dateend, repeatcount не берется в расчет
            if (dateEnd != null && repeatCount == 0 && !string.IsNullOrEmpty(shedule))
            {
                List<IIntervalCondition> condition = new List<IIntervalCondition>();
                try
                {
                    condition = SchedulerFormatter.Default.ParseConditions(shedule);
                }
                catch (Exception ex)
                {
                    throw new ValidationErrors(new PropertyError("shedule", $"неверное значение shedule:{shedule} " + ex.Message));
                }

                var startTimes = condition.GetDatesStarts(startTime.Value, dateEnd.Value.AddMinutes(currentTimeZone.OffsetMinute));
                currentSheduleList = startTimes.Select(c => new SheduleList { StartTime = c, EndTime = c.AddMinutes(duration) }).ToList();
            }

            // dateend и repeatcount - могут передаваться как dateend != NULL и repeatcount != 0, рассчитываем, что наступит раньше и бронирование считается до наименьшего значения
            if (dateEnd != null && repeatCount != 0 && !string.IsNullOrEmpty(shedule))
            {
                List<IIntervalCondition> condition = new List<IIntervalCondition>();
                try
                {
                    condition = SchedulerFormatter.Default.ParseConditions(shedule);
                }
                catch (Exception ex)
                {
                    throw new ValidationErrors(new PropertyError("shedule", $"неверное значение shedule:{shedule} " + ex.Message));
                }

                var startTimes = condition.GetDatesStarts(startTime.Value, dateEnd.Value.AddMinutes(currentTimeZone.OffsetMinute));

                if (startTimes.Count() > repeatCount)
                {
                    startTimes = startTimes.Take(repeatCount);
                }

                if (startTimes.Count() < repeatCount)
                {
                    startTimes = condition.GetDatesStarts(startTime.Value, dateEnd.Value.AddMinutes(currentTimeZone.OffsetMinute).AddYears(1));
                }

                currentSheduleList = startTimes.Select(c => new SheduleList { StartTime = c, EndTime = c.AddMinutes(duration) }).ToList();
            }

            var allBooking = await UnitOfWork.BookingRepository.GetBySpaceIdAsync(spaceId);
            
            if (bookingId.HasValue)
            {
                allBooking = allBooking.Where(c => c.Id != bookingId.Value).ToList();
            }

            foreach (var el in allBooking)
            {
                var baseTimeZone = allTimeZone.FirstOrDefault(c => c.Id == timeZone);

                if (baseTimeZone == null)
                {
                    throw new ValidationErrors(new PropertyError("TimeZone", "Временная зона не найдена"));
                }

                int elDuration = 0;
                if (el.Duration.HasValue)
                {
                    elDuration = el.Duration.Value;
                }

                DateTime startTime2 = startTime.Value;
                if (el.DateStart.HasValue)
                {
                    startTime2 = el.DateStart.Value;
                }

                startTime2 = startTime2.AddMinutes(baseTimeZone.OffsetMinute);

                List<SheduleList> baseSheduleList = new List<SheduleList>();

                // dateend и repeatcount - могут передаваться как dateend = NULL и repeatcount = 0, это означает, что бронирование не периодическое и выполняется один раз. При таких параметрах schedule будет пустой (schedule = "")
                if (string.IsNullOrEmpty(el.Schedule) && el.DateEnd == null && el.RepeatCount == 0)
                {
                    baseSheduleList.Add(new SheduleList { StartTime = startTime2, EndTime = startTime2.AddMinutes(el.Duration.Value) });
                }

                // dateend и repeatcount - могут передаваться как dateend != NULL и repeatcount = 0, это означает, учитывается только dateend, repeatcount не берется в расчет
                if (el.DateEnd != null && el.RepeatCount == 0 && !string.IsNullOrEmpty(el.Schedule))
                {
                    List<IIntervalCondition> condition = new List<IIntervalCondition>();
                    try
                    {
                        condition = SchedulerFormatter.Default.ParseConditions(el.Schedule);
                    }
                    catch (Exception ex)
                    {
                        throw new ValidationErrors(new PropertyError("shedule", $"неверное значение shedule:{shedule} " + ex.Message));
                    }

                    var startTimes = condition.GetDatesStarts(startTime2, el.DateEnd.Value.AddMinutes(baseTimeZone.OffsetMinute));
                    baseSheduleList = startTimes.Select(c => new SheduleList { StartTime = c, EndTime = c.AddMinutes(el.Duration.Value) }).ToList();
                }

                // dateend и repeatcount - могут передаваться как dateend != NULL и repeatcount != 0, рассчитываем, что наступит раньше и бронирование считается до наименьшего значения
                if (el.DateEnd != null && el.RepeatCount != 0 && !string.IsNullOrEmpty(el.Schedule))
                {
                    List<IIntervalCondition> condition = new List<IIntervalCondition>();
                    try
                    {
                        condition = SchedulerFormatter.Default.ParseConditions(el.Schedule);
                    }
                    catch (Exception ex)
                    {
                        throw new ValidationErrors(new PropertyError("shedule", $"неверное значение shedule:{shedule} " + ex.Message));
                    }

                    var startTimes = condition.GetDatesStarts(startTime2, el.DateEnd.Value.AddMinutes(baseTimeZone.OffsetMinute));

                    if (startTimes.Count() > el.RepeatCount)
                    {
                        startTimes = startTimes.Take(el.RepeatCount.Value);
                    }

                    if (startTimes.Count() < el.RepeatCount)
                    {
                        startTimes = condition.GetDatesStarts(startTime2, el.DateEnd.Value.AddMinutes(baseTimeZone.OffsetMinute).AddYears(1));
                    }

                    startTimes.Select(c => new SheduleList { StartTime = c, EndTime = c.AddMinutes(el.Duration.Value) }).ToList();
                }

                IEnumerable<SheduleList> result = currentSheduleList.Intersect(baseSheduleList, new SheduleListComparer());

                if (result.Count() > 0)
                    throw new ValidationErrors(new GeneralError($"Найдено пересечение с конференцией \"{el.Name}\" задано расписание на {el.DateStart.Value:dd.MM.yyyy HH:mm:ss}"));
            }
        }

        /// <summary>
        /// Проверяет связь на удаление.
        /// </summary>
        /// <param name="link">Проверяемая связь.</param>
        /// <param name="deleteItems">Удаялемые связи.</param>
        /// <returns>Результат проверки: <see langword="true"/> - связь не удаляется, <see langword="false"/> - связь удаляется.</returns>
        private static bool IsNotInDeleteList(LinkBookingTovksUsersOther link, List<LinkBookingTovksUsersOther> deleteItems)
        {
            return !deleteItems.Any(dl => dl.VksUsersOtherId == link.VksUsersOtherId);
        }

        /// <summary>
        /// Проверяет привязанных к добаляемому бронированию внешних участников.
        /// </summary>
        /// <param name="item">Данные о бронировании.</param>
        /// <returns>Асинхронная задача.</returns>
        private async Task CheckAddLinkToUsersOtherAsync(Booking item)
        {
            if (item.LinkBookingTovksUsersOthers.Any())
            {
                var saveItems = new List<VksUsersOther>();
                var addItems = new List<LinkBookingTovksUsersOther>();

                foreach (var link in item.LinkBookingTovksUsersOthers)
                {
                    if (link.VksUsersOther != null)
                    {
                        link.VksUsersOther.Email = link.VksUsersOther.Email ?? string.Empty;
                        link.VksUsersOther.Name = link.VksUsersOther.Name ?? string.Empty;
                    }

                    if (await LinkBookingTovksUsersOtherCheckUriAsync(link.VksUsersOther.Uri, link.VksUsersOtherId ?? 0))
                    {
                        throw new ValidationErrors(new GeneralError($"URI участника \"{link.VksUsersOther.Uri}\" уже существует в системе. Выберите существующий или укажите уникальное значение для URI участника."));
                    }

                    if (link.VksUsersOtherId > 0)
                    {
                        addItems.Add(link);
                        saveItems.Add(link.VksUsersOther);

                        link.VksUsersOther = null;
                    }
                    else
                    {
                        addItems.Add(link);
                    }
                }

                item.LinkBookingTovksUsersOthers = addItems;

                await UnitOfWork.VksUsersOtherRepository.SaveRangeAsync(saveItems);
            }
        }

        /// <summary>
        /// Проверяет привязанных к добаляемому бронированию участников.
        /// </summary>
        /// <param name="item">Данные о бронировании.</param>
        /// <returns>Асинхронная задача.</returns>
        private async Task CheckAddLinkToPrincipalAsync(Booking item)
        {
            if (item.LinkBookingToParticipants.Any())
            {
                var addItems = new List<LinkBookingToParticipant>();

                foreach (var link in item.LinkBookingToParticipants)
                {
                    if (link.VksParticipantId > 0)
                    {
                        var user = await UnitOfWork.VksUserRepository.GetByIdAsync(link.VksParticipantId);

                        if (user == null)
                        {
                            link.VksParticipantId = 0;
                            link.VksParticipant.Id = 0;

                            addItems.Add(link);
                        }
                        else
                        {
                            addItems.Add(link);

                            link.VksParticipant = null;
                        }
                    }
                    else
                    {
                        addItems.Add(link);
                    }
                }

                item.LinkBookingToParticipants = addItems;
            }
        }

        /// <summary>
        /// Проверяет привязанных к обновляемому бронированию внешних участников.
        /// </summary>
        /// <param name="updatingBooking">Данные о бронировании.</param>
        /// <returns>Асинхронная задача.</returns>
        private async Task CheckUpdateLinkToUsersOtherAsync(Booking updatingBooking)
        {
            var existsLinks = await UnitOfWork.LinkBookingTovksUsersOthersRepository.AllByBookingAsync(updatingBooking.Id);

            if (updatingBooking.LinkBookingTovksUsersOthers.Any())
            {
                var deleteItems = existsLinks.Where(l => !updatingBooking.LinkBookingTovksUsersOthers.Any(bl => bl.VksUsersOtherId == l.VksUsersOtherId)).ToList();
                deleteItems.ForEach(i => i.VksUsersOther = null);

                var saveItems = new List<VksUsersOther>();
                var addItems = new List<LinkBookingTovksUsersOther>();

                foreach (var link in updatingBooking.LinkBookingTovksUsersOthers.Where(l => IsNotInDeleteList(l, deleteItems)))
                {
                    if (link.VksUsersOther != null)
                    {
                        link.VksUsersOther.Email = link.VksUsersOther.Email ?? string.Empty;
                        link.VksUsersOther.Name = link.VksUsersOther.Name ?? string.Empty;
                    }

                    var user = link.VksUsersOtherId > 0
                            ? await UnitOfWork.VksUsersOtherRepository.GetByIdAsync(link.VksUsersOtherId)
                            : await UnitOfWork.VksUsersOtherRepository.GetByUriAsync(link.VksUsersOther.Uri);

                    if (await LinkBookingTovksUsersOtherCheckUriAsync(link.VksUsersOther.Uri, user?.Id ?? 0))
                    {
                        throw new ValidationErrors(new PropertyError(nameof(Booking.LinkBookingTovksUsersOthers), $"Значение \"{link.VksUsersOther.Uri}\" уже имеется в базе."));
                    }

                    if (user != null)
                    {
                        var saveLink = existsLinks.FirstOrDefault(l => l.VksUsersOtherId == link.VksUsersOtherId) ?? link;

                        user.Email = link.VksUsersOther.Email;
                        user.Name = link.VksUsersOther.Name;

                        saveLink.VksUsersOtherId = user.Id;

                        addItems.Add(saveLink);
                        saveItems.Add(user);

                        link.VksUsersOther = null;
                        saveLink.VksUsersOther = null;
                    }
                    else
                    {
                        addItems.Add(link);
                    }
                }

                updatingBooking.LinkBookingTovksUsersOthers = addItems;

                await UnitOfWork.VksUsersOtherRepository.SaveRangeAsync(saveItems);
                await UnitOfWork.LinkBookingTovksUsersOthersRepository.DeleteRangeAsync(deleteItems);
            }
        }

        /// <summary>
        /// Название комнтаы
        /// </summary>
        /// <param name="spaceId"></param>
        /// <returns></returns>
        private async Task<string> SpaceName(int? spaceId)
        {
            string result = string.Empty;

            if (spaceId == null) return result;

            var space = await UnitOfWork.SpaceRepository.GetByIdAsync(spaceId);
            if (space != null)
            {
                result = space.Name;
            }

            return result;
        }

        private string UidGenerator()
        {
            var uuid = "040000008200E00074C5B7101A82E0080000000050F1C9313CDAD701000000000000000010000000";
            char[] chars = new char[62];
            chars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[16];
                crypto.GetNonZeroBytes(data);
            }

            StringBuilder keyString = new StringBuilder(16);
            foreach (byte b in data)
            {
                //keyString.Append(chars[b % (chars.Length)]);
                keyString.Append(b.ToString("X2"));
            }

            uuid += keyString.ToString();
            return uuid;
        }
    }
}