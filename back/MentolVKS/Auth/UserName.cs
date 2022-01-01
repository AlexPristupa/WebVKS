using MentolVKS.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentolVKS.Auth
{
    /// <summary>
    /// Получение имени пользователя для передачи в сервисы
    /// </summary>
    public class UserName : IUserInterface
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<UserName> _logger;

        public UserName(IHttpContextAccessor httpContextAccessor, ILogger<UserName> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public string GetUserName()
        {
            try
            {
               var name = _httpContextAccessor.HttpContext.User.Identity.Name;
               
               return  string.IsNullOrEmpty(name)? "SYSTEM" : name;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return "SYSTEM";
            }
        }

        public string GetUserIp()
        {
            try
            {
                return _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return String.Empty;
            }
        }
    }
}
