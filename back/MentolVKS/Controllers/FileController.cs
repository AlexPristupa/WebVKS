using MentolVKS.Auth;
using MentolVKS.Common;
using MentolVKS.Model.Auth;
using MentolVKS.Model.Error;
using MentolVKS.Model.Report;
using MentolVKS.Service.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MentolVKS.Controllers
{
    /// <summary>
    /// Контроллер для работы с файлами
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ApiControllerBase
    {
        private ILogger _logger;

        public FileController(IService service, ILogger<FileController> logger) : base(service)
        {
            _logger = logger;
        }
        // GET: api/<TestController>
        [HttpGet("recording")]
        public async Task<ActionResult> Get(int id)
        {
            var recording = await Service.GetRecordingByIdAsync(id);
            if (recording == null)
                return NotFound();

            if (!System.IO.File.Exists(recording.Url)){
                return NotFound();
            }

            FileStream fs = new FileStream(recording.Url, FileMode.Open);
            string fileType = "video/mp4";
            string fileName = string.IsNullOrEmpty(Path.GetFileName(recording.Url)) ? "default.mp4" : Path.GetFileName(recording.Url);

            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(fileName, out fileType))
            {
                fileType = "video/mp4";
            }
            this.Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
            return File(fs,fileType, fileName);
        }
    }
}
