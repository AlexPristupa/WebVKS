using MentolVKS.Model.BaseModel;
using MentolVKS.Model.Filters.Dto;
using MentolVKS.Service.Contract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace MentolVKS.Common
{
    /// <summary>
    /// Базовый контроллер для API
    /// </summary>
    [Microsoft.AspNetCore.Mvc.ApiExplorerSettings(IgnoreApi = false)]
    [Microsoft.AspNetCore.Authorization.Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : Controller
    {
        /// <summary>
        /// Текущий пользователь
        /// </summary>
        private AspNetUser _user;
        
        protected readonly IService Service;
        public ApiControllerBase(IService service)
        {
            Service = service;            
        }

        /// <summary>
        /// Возвращает текущего пользователя
        /// </summary>
        protected async Task<AspNetUser> GetCurrentUserAsync()
        {
            var user = _user ?? (_user = await Service.GetUserByLoginAsync(User.Identity.Name.ToUpper()));

            return user;
        }

        /// <summary> A wrapper method for calling the ModelState.AddModelError() method.  </summary>
        /// <param name="key">The key of the ModelStateEntry to add errors to.</param>
        /// <param name="errorMessage">The error message to add.</param>
        protected void AddModelError(string errorMessage, string key = null)
        {
            key ??= string.Empty;
            ModelState.AddModelError(key, errorMessage);
        }

        /// <summary>
        /// Основная валидация через атрибуты в DTO (True - нет ошибки. False - есть ошибка).
        /// </summary>
        /// <returns>Результат валидации. True - нет ошибки. False - есть ошибка.</returns>
        protected bool Valid() => ModelState.IsValid; //выходим из метода, чтобы вывести все ошибки валидации DTO-шки, переданной в метод контроллера.

        /// <summary>
        /// Дополнительная валидация в методе контроллера (True - нет ошибки. False - есть ошибка).
        /// </summary>
        /// <param name="statement">Утверждение.</param>
        /// <param name="consequence">Следствие.</param>
        /// <returns>Результат валидации. True - нет ошибки. False - есть ошибка.</returns>
        protected bool Valid(bool statement, string consequence)
        {
            if (statement)
                return true;
            AddModelError(consequence);

            return false;
        }

        /// <summary>
        /// Подготовить и применить фабрику запросов
        /// </summary>
        /// <param name="listQuery">dto</param>
        /// <returns></returns>
        protected async Task<dynamic> PreLoadFactoryAsync(FilterQuery listQuery)
        {
            var user = await GetCurrentUserAsync();

            return await Service.AgGridVueGetData(listQuery, user.Id);
        }
    }
}
