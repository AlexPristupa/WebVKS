using MentolVKS.Auth;
using MentolVKS.Auth.Jwt;
using MentolVKS.Common;
using MentolVKS.LdapAuth;
using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using MentolVKS.Model.Validation;
using MentolVKS.Model.ViewModel;
using MentolVKS.Service.Contract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentolVKS.Controllers
{
    /// <summary>
    /// Авторизация
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILogger _logger;
        private readonly IService _service;
        IStringLocalizer<LoginController> _localizer;
        IWebHostEnvironment _env;
        private readonly ILdapAuthInterface _ldap;
        private readonly SessionSettings _sessionSettings;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="service"></param>
        /// <param name="userManager"></param>
        /// <param name="localizer"></param>
        /// <param name="env"></param>
        public LoginController(ILogger<LoginController> logger,
            IService service,            
            IStringLocalizer<LoginController> localizer, 
            IWebHostEnvironment env, 
            ILdapAuthInterface ldap, 
            IOptions<SessionSettings> sessionSettings
            )
        {
            _logger = logger;
            _localizer = localizer;
            _env = env;
            _service = service;
            _ldap = ldap;
            _sessionSettings = sessionSettings.Value;
        }

        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxOperationResult> Post(AuthModel item)
        {
            if (!ModelState.IsValid)
            {
                return AjaxOperationResult.Error(ModelState);
            }

            try
            {
                var result = await _service.AuthUserAsync(item.Login, item.Password, item.Provider);
                result.AccessToken = AuthOptions.GenerateToken(result.UserName, result.Roles, _sessionSettings.IdleTimeOutMinute);                
                result.TimeOutMinute = _sessionSettings.IdleTimeOutMinute;

                result.RefreshToken = await _service.SaveRefreshTokenAsync(result.Id, _sessionSettings.IdleRefreshMinute,"", HttpContext.Connection.RemoteIpAddress.ToString());
                HttpContext.Response.Cookies.Append("refresh_token", result.RefreshToken);              

                return AjaxOperationResult.Success(result);               
            }
            catch (ValidationErrors propertyErrors)
            {
                ModelState.AddValidationErrors(propertyErrors);
            }

            return AjaxOperationResult.Error(ModelState);
        }

        /// <summary>
        /// Выход из системы
        /// </summary>
        /// <returns></returns>
        [HttpPost("LogOut")]
        public async Task<AjaxOperationResult> LogOut()
        {
            var refresh = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "refresh_token");
            
            if (string.IsNullOrEmpty(refresh.Value))
                return AjaxOperationResult.Error(_localizer["Auth Error"]);

            await _service.DeleteRefreshTokenAsync(refresh.Value);

            return AjaxOperationResult.Success();
        }

        /// <summary>
        /// Обновить token
        /// </summary>
        /// <returns></returns>
        [HttpGet("Refresh")]
        public async Task<AjaxOperationResult> RefreshToken(string refresh)
        {
            try
            {
                var result = new IdentityModel();
                if (string.IsNullOrEmpty(refresh))
                    return AjaxOperationResult.Error(_localizer["Auth Error"]);
                //refresh = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "refresh_token");

                if (string.IsNullOrEmpty(refresh))
                    return AjaxOperationResult.Error(_localizer["Auth Error"]);

                var token = await _service.GetRefreshTokenAsync(refresh);

                if (token == null)
                    return AjaxOperationResult.Error(_localizer["Auth Error"]);

                result.AccessToken = AuthOptions.GenerateToken(token.UserName, token.Roles, _sessionSettings.IdleTimeOutMinute);

                return AjaxOperationResult.Success(result);
            }
            catch
            {
                return AjaxOperationResult.Error(_localizer["Auth Error"]);
            }
        }
    }
}
