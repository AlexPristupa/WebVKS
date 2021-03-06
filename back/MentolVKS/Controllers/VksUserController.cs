using AutoMapper;
using MentolVKS.Common;
using MentolVKS.Model.BaseModel;
using MentolVKS.Models;
using MentolVKS.Service.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MentolVKS.Controllers
{
    /// <summary>
    /// Контроллер для теста
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class VksUserController : ApiControllerBase
    {
        private ILogger _logger;
        private IMapper _mapper;

        public VksUserController(IService service, ILogger<VksUserController> logger) : base(service)
        {
            _logger = logger;
            _mapper = new MapperConfiguration(cfg => {
                cfg.AllowNullCollections = true;
                cfg.AllowNullDestinationValues = true;
                cfg.CreateMap<VksUser, VksUserPutViewModel>()
                    .ForMember(nameof(VksUserPutViewModel.VksUserName), opt => opt.MapFrom(src => src.Name))
                    .ForMember(nameof(VksUserPutViewModel.Uri), opt => opt.MapFrom(src => src.Phone));
                cfg.CreateMap<VksUserPutViewModel, VksUser>();
            }).CreateMapper();
        }

        [HttpGet("GetByJid")]
        public async Task<AjaxOperationResult> Jid(string jid)
        {
            var item =await Service.GetVksUserByJidAsync(jid);
            List<VksUserJidViewModel> result = new List<VksUserJidViewModel>();

            if (item != null)
            {
                result.Add(new VksUserJidViewModel
                {
                    Id = item.Id,
                    Name = item.Name + " (" + item.JID + ")",
                    Jid = item.JID
                });
            }

            return AjaxOperationResult.Success(result);
        }

        /// <summary>
        /// Получить участника
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ResponseViewModel<VksUser>), StatusCodes.Status200OK)]
        public async Task<AjaxOperationResult> Get(int id)
        {
            var baseResult = await Service.GetVksUserByIdAsync(id);
            var result = _mapper.Map<VksUserPutViewModel>(baseResult);

            return AjaxOperationResult.Success(result);
        }
    }
}
