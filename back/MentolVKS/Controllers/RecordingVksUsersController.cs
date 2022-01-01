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
    /// Бронирование - Записи
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RecordingVksUsersController : ApiControllerBase
    {
        private ILogger _logger;
        private IMapper _mapper;
        IStringLocalizer<RecordingVksUsersController> _localizer;

        public RecordingVksUsersController(IService service, ILogger<RecordingVksUsersController> logger, IStringLocalizer<RecordingVksUsersController> localizer) : base(service)
        {
            _logger = logger;
            _localizer = localizer;
            _mapper = new MapperConfiguration(cfg => {
                cfg.AllowNullCollections = true;
                cfg.AllowNullDestinationValues = true;
                cfg.CreateMap<RecordingVksUsers, RecordingVksUsersPutViewModel>();
                cfg.CreateMap<RecordingVksUsersPostViewModel, RecordingVksUsers>();
                cfg.CreateMap<RecordingVksUsersPutViewModel, RecordingVksUsers>();
            }).CreateMapper();
        }

        /// <summary>
        /// Добавление прав на записи
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseViewModel<RecordingVksUsers>), StatusCodes.Status200OK)]
        public async Task<AjaxOperationResult> Post([FromBody] RecordingVksUsersPostViewModel item)
        {
            if (ModelState.IsValid)
            {
                try
                {                    
                    var result = await Service.AddRecordingVksUsersAsync(_mapper.Map<RecordingVksUsers>(item));

                    return AjaxOperationResult.Success(_mapper.Map<RecordingVksUsersPutViewModel>(result));
                }
                catch (ValidationErrors propertyErrors)
                {
                    ModelState.AddValidationErrors(propertyErrors);
                }
            }
            
            return AjaxOperationResult.Error(ModelState);
        }

        /// <summary>
        /// Редактирование прав на записи
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(ResponseViewModel<RecordingVksUsers>), StatusCodes.Status200OK)]
        public async Task<AjaxOperationResult> Put([FromBody] RecordingVksUsersPutViewModel item)
        {
            try
            {
                var result = await Service.UpdateRecordingVksUsersAsync(_mapper.Map<RecordingVksUsers>(item));

                return AjaxOperationResult.Success(_mapper.Map<RecordingVksUsersPutViewModel>(result));
            }
            catch (ValidationErrors propertyErrors)
            {
                ModelState.AddValidationErrors(propertyErrors);
            }

            return AjaxOperationResult.Error(ModelState);
        }

        /// <summary>
        /// Удаление прав на записи
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        [ProducesResponseType(typeof(AjaxOperationResult), StatusCodes.Status200OK)]
        public async Task<AjaxOperationResult> Delete(int id)
        {
            try
            {
                await Service.DeleteRecordingVksUsersAsync(id);

                return AjaxOperationResult.Success(message: _localizer["Record deleted"]);
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
