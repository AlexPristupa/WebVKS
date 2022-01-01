using AutoMapper;
using MentolVKS.Common;
using MentolVKS.Model.Validation;
using MentolVKS.Service.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MentolVKS.Controllers
{
    /// <summary>
    /// Бронирование - Записи
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RecordingController : ApiControllerBase
    {
        private ILogger _logger;
        private IMapper _mapper;
        IStringLocalizer<RecordingController> _localizer;

        public RecordingController(IService service, ILogger<RecordingController> logger, IStringLocalizer<RecordingController> localizer) : base(service)
        {
            _logger = logger;
            _localizer = localizer;
            _mapper = new MapperConfiguration(cfg => {
                cfg.AllowNullCollections = true;
                cfg.AllowNullDestinationValues = true;
            }).CreateMapper();
        }

        /// <summary>
        /// Удалить запись
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        [ProducesResponseType(typeof(AjaxOperationResult), StatusCodes.Status200OK)]
        public async Task<AjaxOperationResult> Delete(int id)
        {
            try
            {
                await Service.DeleteRecordingAsync(id);

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
