using LogicCore.Tasking;
using LogicCore.Tasking.Scheduler;
using LogicCore.Tasking.Scheduler.Conditions;
using MentolVKS.Auth;
using MentolVKS.Common;
using MentolVKS.Model.Report;
using MentolVKS.Models;
using MentolVKS.Service.Contract;
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
    /// Контроллер для теста
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AboutController : ApiControllerBase
    {
        private ILogger _logger;

        public AboutController(IService service, ILogger<AboutController> logger) : base(service)
        {
            _logger = logger;
        }
        // GET: api/<TestController>
        [HttpGet]
        public async Task<AjaxOperationResult> Get()
        {
            var license =  await Service.LicenseXmlGetFromBaseAsync();
            
            AboutViewModel result = new AboutViewModel();
            result.DateEnd = license.DateEnd;
            result.DateStart = license.DateStart;
            result.SerialNumber = license.SerialNumber;
            result.Products = license.Products;

            return AjaxOperationResult.Success(result);
        }
    }
}
