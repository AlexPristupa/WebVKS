using MentolVKS.Auth;
using MentolVKS.Common;
using MentolVKS.Model.Auth;
using MentolVKS.Model.Error;
using MentolVKS.Model.Report;
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
    public class TestController : ApiControllerBase
    {
        private ILogger _logger;

        public TestController(IService service, ILogger<TestController> logger) : base(service)
        {
            _logger = logger;
        }
        // GET: api/<TestController>
        [HttpGet]
        [AllowedRoles(Role.MMS_BOOKING)]
        public async Task<AjaxOperationResult> Get(string test)
        {
            var result = new ValidationError();


            result.AddFieldError("test", "test", "test");
            result.AddFieldError("test", "test1", "test1");
            result.AddFieldError("test3", "test1", "test1");
            result.AddFormError("test", "test");
            result.AddFormError("test4", "test4");
            /*var buf = new Error();
            buf.Field = "test";
            buf.Errors.Add(new ErrorDetail { Exception = "test", Message = "test" });

            result.Validation.Add(buf);*/


            return AjaxOperationResult.Error("",result);
        }

        // GET api/<TestController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TestController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TestController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TestController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
