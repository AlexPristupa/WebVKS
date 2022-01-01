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
using System;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MentolVKS.Controllers
{
    /// <summary>
    /// Бронирование - Комнаты
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SpaceController : ApiControllerBase
    {
        private ILogger _logger;
        private IMapper _mapper;

        public SpaceController(IService service, ILogger<SpaceController> logger, IStringLocalizer<SpaceController> localizer) : base(service)
        {
            _logger = logger;
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AllowNullCollections = true;
                cfg.AllowNullDestinationValues = true;

                cfg.CreateMap<Space, SpacePutViewModel>().ForMember("LinkSpaceToParticipants", opt => opt.MapFrom(src => src.LinkSpaceToParticipants.Select(c => new LinkSpaceToParticipantView
                {
                    CallLegProfileGuid = c.CallLegProfileGuid,
                    CanAddRemoveMember = c.CanAddRemoveMember,
                    CanChangeCallId = c.CanChangeCallId,
                    CanChangeName = c.CanChangeName,
                    CanChangeNonMemberAccessAllowed = c.CanChangeNonMemberAccessAllowed,
                    CanChangePassCode = c.CanChangePassCode,
                    CanChangeUri = c.CanChangeUri,
                    CanDestroy = c.CanDestroy,
                    CanRemoveSelf = c.CanRemoveSelf,
                    VksUserId = c.VksUserId,
                })));

                cfg.CreateMap<SpacePostViewModel, Space>().ForMember("LinkSpaceToParticipants", opt=> opt.MapFrom(src=> src.LinkSpaceToParticipants.Select(c=> new LinkSpaceToParticipant { 
                  CallLegProfileGuid = c.CallLegProfileGuid,
                  CanAddRemoveMember = c.CanAddRemoveMember,
                  CanChangeCallId = c.CanChangeCallId,
                  CanChangeName = c.CanChangeName,
                  CanChangeNonMemberAccessAllowed =c.CanChangeNonMemberAccessAllowed,
                  CanChangePassCode = c.CanChangePassCode,
                  CanChangeUri = c.CanChangeUri,
                  CanDestroy = c.CanDestroy,
                  CanRemoveSelf = c.CanRemoveSelf,
                  VksUserId =c.VksUserId,                                           
                })));

                cfg.CreateMap<SpacePutViewModel, Space>().ForMember("LinkSpaceToParticipants", opt => opt.MapFrom(src => src.LinkSpaceToParticipants.Select(c => new LinkSpaceToParticipant
                {
                    CallLegProfileGuid = c.CallLegProfileGuid,
                    CanAddRemoveMember = c.CanAddRemoveMember,
                    CanChangeCallId = c.CanChangeCallId,
                    CanChangeName = c.CanChangeName,
                    CanChangeNonMemberAccessAllowed = c.CanChangeNonMemberAccessAllowed,
                    CanChangePassCode = c.CanChangePassCode,
                    CanChangeUri = c.CanChangeUri,
                    CanDestroy = c.CanDestroy,
                    CanRemoveSelf = c.CanRemoveSelf,
                    VksUserId = c.VksUserId,
                    SpaceId = src.Id
                })));
            }).CreateMapper();
        }

        /// <summary>
        /// Получить комнату
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ResponseViewModel<Space>), StatusCodes.Status200OK)]
        public async Task<AjaxOperationResult> Get(int id)
        {
            var baseResult = await Service.GetSpaceByIdAsync(id);
            var result = _mapper.Map<SpacePutViewModel>(baseResult);

            return AjaxOperationResult.Success(result);
        }


        /// <summary>
        /// Добавить комнату
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseViewModel<Space>), StatusCodes.Status200OK)]
        public async Task<AjaxOperationResult> Post([FromBody] SpacePostViewModel item)
        {
            if (ModelState.IsValid)
            {
                try
                {                    
                    var baseResult = await Service.AddSpaceAsync(_mapper.Map<Space>(item));
                    var result = _mapper.Map<SpacePutViewModel>(baseResult);

                    return AjaxOperationResult.Success(result);
                }
                catch (ValidationErrors propertyErrors)
                {
                    ModelState.AddValidationErrors(propertyErrors);
                }
            }

            return AjaxOperationResult.Error(ModelState);
        }

        /// <summary>
        /// Обнавить комнату
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(ResponseViewModel<Space>), StatusCodes.Status200OK)]
        public async Task<AjaxOperationResult> Put([FromBody] SpacePutViewModel item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var baseResult = await Service.UpdateSpaceAsync(_mapper.Map<Space>(item));
                    var result = _mapper.Map<SpacePutViewModel>(baseResult);

                    return AjaxOperationResult.Success(result);
                }
                catch (ValidationErrors propertyErrors)
                {
                    ModelState.AddValidationErrors(propertyErrors);
                }
            }

            return AjaxOperationResult.Error(ModelState);
        }

        /// <summary>
        /// Удалить комнату
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        [ProducesResponseType(typeof(AjaxOperationResult), StatusCodes.Status200OK)]
        public async Task<AjaxOperationResult> Delete(int id)
        {
            
            try
            {
                await Service.DeleteSpaceAsync(id);

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
    }
}
