using LogicCore.Tasking.Scheduler;
using LogicCore.Tasking.Scheduler.Conditions;
using MentolVKS.Auth;
using MentolVKS.Common;
using MentolVKS.Model.BaseModel;
using MentolVKS.Model.Report;
using MentolVKS.Model.Validation;
using MentolVKS.Models;
using MentolVKS.Service.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MentolVKS.Controllers
{
    /// <summary>
    /// Расписания
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ApiControllerBase
    {
        private ILogger _logger;

        public ScheduleController(IService service, ILogger<ScheduleController> logger) : base(service)
        {
            _logger = logger;
        }
       
        /// <summary>
        /// Отображение расписания
        /// </summary>
        /// <param name="search"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpPost("ScheduleText")]
        [ProducesResponseType(typeof(ResponseViewModel<ScheduleOutputViewModel>), StatusCodes.Status200OK)]
        public async Task<AjaxOperationResult> SheduleText(ScheduleInputViewModel item)
        {
            if (!ModelState.IsValid)
            {
                return AjaxOperationResult.Error("", ModelState);
            }

            ScheduleOutputViewModel result = new ScheduleOutputViewModel();

            try
            {
                ReportSchedule schedule = new ReportSchedule(item.Schedule, item.DateStart, item.TimeZone);                
                result.Schedule = item.Schedule;
                result.NameSchedule = item.NameSchedule;
                result.DateStart = item.DateStart;
                result.TimeZone = item.TimeZone;
                result.NextRun = schedule.NextRun();
                result.ScheduleText = schedule.IntervalFormat;
            }
            catch (ValidationErrors propertyErrors)
            {
                ModelState.AddValidationErrors(propertyErrors);

                return AjaxOperationResult.Error("", ModelState);
            }


            return AjaxOperationResult.Success(result);
        }

        /// <summary>
        /// Проверка расписания
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("ScheduleCheck")]
        [ProducesResponseType(typeof(ResponseViewModel<SheduleCheckInputModel>), StatusCodes.Status200OK)]
        public async Task<AjaxOperationResult> SheduleCheck(SheduleCheckInputModel item)
        {           
            try
            {
                await Service.CheckShedule(item.SpaceId, item.Schedule, item.DateStart, item.Duration, item.TimeZone, item.DateEnd, item.RepeatCount, item.BookingId);
            }
            catch (ValidationErrors propertyErrors)
            {
                ModelState.AddValidationErrors(propertyErrors);

                return AjaxOperationResult.Error(ModelState);
            }

            return AjaxOperationResult.Success(item);
        }
    }
}
