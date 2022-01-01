using AutoMapper;
using MentolVKS.Common;
using MentolVKS.Model.BaseModel;
using MentolVKS.Models;
using MentolVKS.Service.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MentolVKS.Controllers
{
    /// <summary>
    /// Справочники
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DirectoryController : ApiControllerBase
    {
        private ILogger _logger;
        private IMapper _mapper;

        /// <inheritdoc/>
        public DirectoryController(IService service, ILogger<DirectoryController> logger, IStringLocalizer<DirectoryController> localizer) : base(service)
        {
            _logger = logger;
        }

        /// <summary>
        /// Комнаты / Space
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Space")]
        [ProducesResponseType(typeof(ResponseViewModel<SelectDirectoryView>), StatusCodes.Status200OK)]
        public async Task<AjaxOperationResult> Space(string search = "", int limit = 300)
        {
            if (String.IsNullOrEmpty(search)) search = "";

            var result = await Service.GetSpaceSelectDirectoryAsync(search,limit);

            return AjaxOperationResult.Success(result);
        }

        /// <summary>
        /// Группа серверов/ServersGroups
        /// </summary>
        /// <param name="search"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("ServerGroups")]
        [ProducesResponseType(typeof(ResponseViewModel<List<SelectDirectoryView>>), StatusCodes.Status200OK)]
        public async Task<AjaxOperationResult> ServerGroups(string search = "", int limit = 300)
        {
            if (String.IsNullOrEmpty(search)) search = "";

            try
            {
                return AjaxOperationResult.Success(await Service.GetServersGroupsSelectDirectoryAsync(search, limit));
            }
            catch (Exception err)
            {
                return AjaxOperationResult.Error(err.Message);
            }
        }

        /// <summary>
        /// PinPolitics
        /// </summary>
        /// <param name="search"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("PinPolitics")]
        [ProducesResponseType(typeof(ResponseViewModel<List<PinPolitics>>), StatusCodes.Status200OK)]
        public async Task<AjaxOperationResult> PinPolitics(string search = "", int limit = 300)
        {
            if (String.IsNullOrEmpty(search)) search = "";

            try
            {
                return AjaxOperationResult.Success(await Service.GetPinPoliticsSelectDirectoryAsync(search, limit));
            }
            catch (Exception err)
            {
                return AjaxOperationResult.Error(err.Message);
            }
        }

        /// <summary>
        /// Тип подключения участников/ConnectionType
        /// </summary>
        /// <param name="search"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("ConnectionType")]
        [ProducesResponseType(typeof(ResponseViewModel<List<SelectDirectoryView>>), StatusCodes.Status200OK)]
        public async Task<AjaxOperationResult> ConnectionType(string search = "", int limit = 300)
        {
            if (String.IsNullOrEmpty(search)) search = "";

            try
            {
                return AjaxOperationResult.Success(await Service.GetConnectionTypeSelectDirectoryAsync(search, limit));
            }
            catch (Exception err)
            {
                return AjaxOperationResult.Error(err.Message);
            }
        }

        /// <summary>
        /// Сервера /VksServer
        /// </summary>
        /// <param name="search"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("VksServer")]
        public async Task<AjaxOperationResult> VksServer(string search = "", int limit = 300)
        {
            if (String.IsNullOrEmpty(search)) search = "";

            try
            {
                return AjaxOperationResult.Success(await Service.GetVksServerSelectDirectoryAsync(search, limit));
            }
            catch (Exception err)
            {
                return AjaxOperationResult.Error(err.Message);
            }
        }

        /// <summary>
        /// Пользователи/VksUser
        /// </summary>
        /// <param name="search"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("VksUser")]
        [ProducesResponseType(typeof(ResponseViewModel<SelectDirectoryView>), StatusCodes.Status200OK)]
        public async Task<AjaxOperationResult> VksUser(string search = "", int limit = 300)
        {
            if (String.IsNullOrEmpty(search)) search = "";

            try
            {
                return AjaxOperationResult.Success(await Service.GetVksUserSelectDirectoryAsync(search, limit));
            }
            catch (Exception err)
            {
                return AjaxOperationResult.Error(err.Message);
            }
        }
       
        /// <summary>
        /// Временные зоны/TimeZone
        /// </summary>
        /// <param name="search"></param>
        /// <param name="limit"></param>
        /// <returns></returns>

        [HttpGet("TimeZone")]
        [ProducesResponseType(typeof(ResponseViewModel<Model.BaseModel.TimeZone>), StatusCodes.Status200OK)]
        public async Task<AjaxOperationResult> TimeZone(string search = "", int limit = 300)
        {      
            if (String.IsNullOrEmpty(search)) search = "";      

            try
            {
                return AjaxOperationResult.Success(await Service.GetTimeZoneSelectDirectoryAsync(search, limit));
            }
            catch (Exception err)
            {
                return AjaxOperationResult.Error(err.Message);
            }
        }

        /// <summary>
        /// Тип конференции/BookingType
        /// </summary>
        /// <param name="search"></param>
        /// <param name="limit"></param>
        /// <returns></returns>        
        [HttpGet("BookingType")]
        [ProducesResponseType(typeof(ResponseViewModel<SelectDirectoryView>), StatusCodes.Status200OK)]
        public async Task<AjaxOperationResult> BookingType(string search = "", int limit = 300)
        {
            if (String.IsNullOrEmpty(search)) search = "";

            try
            {
                return AjaxOperationResult.Success(await Service.GetBookingTypeSelectDirectoryAsync(search, limit));
            }
            catch (Exception err)
            {
                return AjaxOperationResult.Error(err.Message);
            }
        }

        /// <summary>
        /// Пользователи/AspNetUser
        /// </summary>
        /// <param name="search">Значение для поиска</param>
        /// <param name="limit">Ограничение на количество записей</param>
        /// <returns></returns>        
        [HttpGet("AspNetUsers")]
        [ProducesResponseType(typeof(ResponseViewModel<SelectDirectoryView>), StatusCodes.Status200OK)]
        public async Task<AjaxOperationResult> AspNetUsers(string search = "", int limit = 300)
        {
            if (String.IsNullOrEmpty(search)) search = "";

            try
            {
                return AjaxOperationResult.Success(await Service.AspNetUserGetSelectDirectoryAsync(search, limit));
            }
            catch (Exception err)
            {
                return AjaxOperationResult.Error(err.Message);
            }
        }

        /// <summary>
        /// Внешние участники 
        /// </summary>
        /// <param name="search">Значение для поиска</param>
        /// <param name="limit">Ограничение на количество записей</param>
        /// <returns></returns>        
        [HttpGet("VksUserOther")]
        [ProducesResponseType(typeof(ResponseViewModel<SelectDirectoryView>), StatusCodes.Status200OK)]
        public async Task<AjaxOperationResult> VksUserOther(string search = "", int limit = 300)
        {
            if (string.IsNullOrEmpty(search)) search = "";

            try
            {
                return AjaxOperationResult.Success(await Service.GetVksUsersOtherSelectDirectoryAsync(search, limit));
            }
            catch (Exception err)
            {
                return AjaxOperationResult.Error(err.Message);
            }
        }
    }
}
