using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MentolVKS.Common;
using MentolVKS.Model.BaseModel;
using MentolVKS.Model.Validation;
using MentolVKS.Models;
using MentolVKS.Service.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MentolVKS.Controllers
{
    /// <summary>
    /// Контроллер бронирования.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ApiControllerBase
    {
        /// <summary>
        /// Локализация строк
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Маппирование объектов
        /// </summary>
        private readonly IMapper _mapper;

        /// <inheritdoc/>
        public BookingController(IService service, ILogger<BookingController> logger, IStringLocalizer<BookingController> localizer)
            : base(service)
        {
            _logger = logger;
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AllowNullCollections = true;
                cfg.AllowNullDestinationValues = true;
                ConfigurePostMap(cfg);
                ConfigurePutMap(cfg);
                ConfigureGetMap(cfg);
                ConfigureBookingMap(cfg);
            }).CreateMapper();
        }

        /// <summary>
        /// Удалить бронирование
        /// </summary>
        /// <param name="id">Идентификатор удаляемого бронирования</param>
        [HttpDelete]
        [ProducesResponseType(typeof(AjaxOperationResult), StatusCodes.Status200OK)]
        public async Task<AjaxOperationResult> Delete(int id)
        {
            try
            {
                await Service.DeleteBookingAsync(id);

                return AjaxOperationResult.Success();
            }
            catch (ValidationErrors propertyErrors)
            {
                ModelState.AddValidationErrors(propertyErrors);

                return AjaxOperationResult.Error(ModelState);
            }
            catch (Exception ex)
            {
                return AjaxOperationResult.Error(ex.Message);
            }
        }

        /// <summary>
        /// Получить бронирование.
        /// </summary>
        /// <param name="id">Идентификатор бронирования.</param>
        /// <returns>Асинхронная задача с результатом запроса.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ResponseViewModel<BookingFullViewModel>), StatusCodes.Status200OK)]
        public async Task<AjaxOperationResult> Get(int id)
        {
            try
            {
                var baseResult = await Service.GetBookingByIdAsync(id);
                var result = _mapper.Map<BookingFullViewModel>(baseResult);

                return AjaxOperationResult.Success(result);
            }
            catch(Exception e)
            {
                return AjaxOperationResult.Error(e.Message);
            }
        }

        [HttpPost("Outlook")]
        [ProducesResponseType(typeof(ResponseViewModel<string>), StatusCodes.Status200OK)]
        public async Task<AjaxOperationResult> OutlookPost([FromBody] OutlookBookingPostModel item)
        {
            _logger.LogInformation(JsonConvert.SerializeObject(item));
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await GetCurrentUserAsync();
                    var result = await Service.AddOutlookBookingAsync(user.Id, 
                        item.Name, 
                        item.Location, 
                        item.DateStart, 
                        item.TimeZoneMinute, 
                        item.ConnectionTypeId, 
                        item.OpenConferenceBefore, 
                        item.Emails,
                        item.Ics,
                        item.Uid, 
                        item.Duration, 
                        item.Organizer,
                        item.DateEnd,
                        item.TimeZoneName);

                    return AjaxOperationResult.Success(result);
                }
                catch (ValidationErrors propertyErrors)
                {
                    ModelState.AddValidationErrors(propertyErrors);
                }
                catch (Exception ex) {
                    ModelState.AddExceptionErrors(ex);
                    _logger.LogInformation(ex.Message);
                    _logger.LogInformation(ex.InnerException.Message);
                }
            }
            return AjaxOperationResult.Error(ModelState);
        }

        [HttpDelete("OutlookDelete")]
        public async Task<AjaxOperationResult> OutlookDelete(string uuid)
        {
            try
            {
                await Service.DeleteBookingByUuid(uuid);
                return AjaxOperationResult.Success();
            }
            catch
            {
                return AjaxOperationResult.Error();
            }
            return AjaxOperationResult.Success();
        }

        /// <summary>
        /// Добавить бронирование
        /// </summary>
        /// <param name="item">Добавляемое бронирование</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseViewModel<BookingFullViewModel>), StatusCodes.Status200OK)]
        public async Task<AjaxOperationResult> Post([FromBody] BookingPostViewModel item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await Service.AddBookingAsync(_mapper.Map<Booking>(item));

                    return AjaxOperationResult.Success(_mapper.Map<BookingFullViewModel>(result));
                }
                catch (ValidationErrors propertyErrors)
                {
                    ModelState.AddValidationErrors(propertyErrors);
                }
            }

            return AjaxOperationResult.Error(ModelState);
        }

        /// <summary>
        /// Обновить бронирование.
        /// </summary>
        /// <param name="item">Обновляемая бронь.</param>
        /// <returns>Результат выполнения.</returns>
        [HttpPut]
        [ProducesResponseType(typeof(ResponseViewModel<BookingFullViewModel>), StatusCodes.Status200OK)]
        public async Task<AjaxOperationResult> Put([FromBody] BookingPutViewModel item)
        {
            if (!ModelState.IsValid)
            {
                return AjaxOperationResult.Error(ModelState);
            }

            try
            {
                var result = await Service.UpdateBookingAsync(_mapper.Map<Booking>(item));

                return AjaxOperationResult.Success(_mapper.Map<BookingFullViewModel>(result));
            }
            catch (ValidationErrors propertyErrors)
            {
                ModelState.AddValidationErrors(propertyErrors);
            }

            return AjaxOperationResult.Error(ModelState);
        }

        /// <summary>
        /// Конфигурирует маппинг <see cref="BookingPostViewModel"/> на <see cref="Booking"/>.
        /// </summary>
        /// <param name="cfg">Данные конфигурации.</param>
        private static void ConfigurePostMap(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<BookingPostViewModel, Booking>()
                .ForMember(
                    nameof(Booking.LinkBookingToParticipants),
                    opt => opt.MapFrom(src => src.LinkBookingToParticipants.Select(c => new LinkBookingToParticipant
                    {
                        Id = 0,
                        CallLegProfileGuid = c.CallLegProfileGuid,
                        Uri = c.Uri,
                        VksParticipantId = c.VksParticipantId,
                        VksParticipant = new VksUser
                        {
                            Id = c.VksParticipantId,
                            Email = c.Email,
                            Name = c.VksUserName
                        }
                    })))
                .ForMember(
                    nameof(Booking.LinkBookingTovksUsersOthers),
                    opt => opt.MapFrom(src => src.LinkBookingToVksUsersOthers.Select(c => new LinkBookingTovksUsersOther
                    {
                        Id = 0,
                        VksUsersOtherId = c.VksUsersOtherId,
                        VksUsersOther = string.IsNullOrEmpty(c.Uri)
                        ? null
                        : new VksUsersOther
                        {
                            Id = c.VksUsersOtherId ?? 0,
                            Email = c.Email,
                            Name = c.VksUserOtherName,
                            Uri = c.Uri
                        }
                    })));
        }

        /// <summary>
        /// Конфигурирует маппинг <see cref="BookingPutViewModel"/> на <see cref="Booking"/>.
        /// </summary>
        /// <param name="cfg">Данные конфигурации.</param>
        private static void ConfigurePutMap(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<BookingPutViewModel, Booking>()
                .ForMember(
                    nameof(Booking.LinkBookingToParticipants),
                    opt => opt.MapFrom(src => src.LinkBookingToParticipants.Select(c => new LinkBookingToParticipant
                    {
                        Id = 0,
                        CallLegProfileGuid = c.CallLegProfileGuid,
                        Uri = c.Uri,
                        VksParticipantId = c.VksParticipantId,
                        VksParticipant = new VksUser
                        {
                            Id = c.VksParticipantId,
                            Email = c.Email,
                            Name = c.VksUserName
                        }
                    })))
                .ForMember(
                    nameof(Booking.LinkBookingTovksUsersOthers),
                    opt => opt.MapFrom(src => src.LinkBookingToVksUsersOthers.Select(c => new LinkBookingTovksUsersOther
                    {
                        Id = 0,
                        VksUsersOtherId = c.VksUsersOtherId,
                        BookingId = src.Id,
                        VksUsersOther = string.IsNullOrEmpty(c.Uri)
                        ? null
                        : new VksUsersOther
                        {
                            Id = c.VksUsersOtherId ?? 0,
                            Email = c.Email,
                            Name = c.VksUserOtherName,
                            Uri = c.Uri
                        }
                    })));
        }

        /// <summary>
        /// Конфигурирует маппинг <see cref="BookingFullViewModel"/> на <see cref="Booking"/>.
        /// </summary>
        /// <param name="cfg">Данные конфигурации.</param>
        private static void ConfigureGetMap(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<BookingFullViewModel, Booking>()
                .ForMember(
                    nameof(Booking.LinkBookingToParticipants),
                    opt => opt.MapFrom(src => src.LinkBookingToParticipants.Select(c => new LinkBookingToParticipant
                    {
                        Id = 0,
                        CallLegProfileGuid = c.CallLegProfileGuid,
                        Uri = c.Uri,
                        VksParticipantId = c.VksParticipantId,
                        VksParticipant = new VksUser
                        {
                            Id = c.VksParticipantId,
                            Email = c.Email,
                            Name = c.VksUserName
                        }
                    })))
                .ForMember(
                    nameof(Booking.LinkBookingTovksUsersOthers),
                    opt => opt.MapFrom(src => src.LinkBookingToVksUsersOthers.Select(c => new LinkBookingTovksUsersOther
                    {
                        VksUsersOtherId = c.VksUsersOtherId,
                        BookingId = src.Id,
                        VksUsersOther = string.IsNullOrEmpty(c.Uri)
                        ? null
                        : new VksUsersOther
                        {
                            Uri = c.Uri,
                            Name = c.VksUserOtherName,
                            Email = c.Email,
                            Id = c.VksUsersOtherId ?? 0
                        }
                    })));
        }

        /// <summary>
        /// Конфигурирует маппинг <see cref="Booking"/> на <see cref="BookingFullViewModel"/>.
        /// </summary>
        /// <param name="cfg">Данные конфигурации.</param>
        private static void ConfigureBookingMap(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Booking, BookingFullViewModel>()
                .ForMember(
                    nameof(BookingFullViewModel.LinkBookingToParticipants),
                    opt => opt.MapFrom(src => src.LinkBookingToParticipants.Select(c => new LinkBookingToParticipantViewModel
                    {
                        CallLegProfileGuid = c.CallLegProfileGuid,
                        Uri = c.Uri,
                        VksParticipantId = c.VksParticipantId,
                        Email = c.VksParticipant == null ? string.Empty : c.VksParticipant.Email ?? string.Empty,
                        VksUserName = c.VksParticipant == null ? string.Empty : c.VksParticipant.Name ?? string.Empty
                    })))
                .ForMember(
                    nameof(BookingFullViewModel.LinkBookingToVksUsersOthers),
                    opt => opt.MapFrom(src => src.LinkBookingTovksUsersOthers.Select(c => new LinkBookingToVksUsersOtherViewModel
                    {
                        VksUsersOtherId = c.VksUsersOtherId,
                        VksUserOtherName = c.VksUsersOther == null ? string.Empty : c.VksUsersOther.Name,
                        Uri = c.VksUsersOther == null ? string.Empty : c.VksUsersOther.Uri,
                        Email = c.VksUsersOther == null ? string.Empty : c.VksUsersOther.Email
                    })));
        }
    }
}