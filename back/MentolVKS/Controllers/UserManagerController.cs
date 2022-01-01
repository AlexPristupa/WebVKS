using AutoMapper;
using MentolVKS.Auth;
using MentolVKS.Common;
using MentolVKS.LdapAuth;
using MentolVKS.Model.Auth;
using MentolVKS.Model.BaseModel;
using MentolVKS.Model.Error;
using MentolVKS.Model.Report;
using MentolVKS.Model.Validation;
using MentolVKS.Model.ViewModel;
using MentolVKS.Models;
using MentolVKS.Service.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
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
    public class UserManagerController : ApiControllerBase
    {
        private ILogger _logger;
        private IMapper _mapper;
        IStringLocalizer<UserManagerController> _localizer;
        private readonly ILdapAuthInterface _ldap;

        public UserManagerController(IService service, ILogger<UserManagerController> logger, ILdapAuthInterface ldap, IStringLocalizer<UserManagerController> localizer) : base(service)
        {           
            _logger = logger;
            _ldap = ldap;
            _localizer = localizer;

            _mapper = new MapperConfiguration(cfg => {
                cfg.AllowNullCollections = true;
                cfg.AllowNullDestinationValues = true;
                cfg.CreateMap<UserModel, AspNetUser>();
                cfg.CreateMap<RoleViewModel, AspNetRole>();
                cfg.CreateMap<AspNetRole, RoleViewModel>()
                 .ForMember("Name", opt => opt.MapFrom(src => src.Name))
                 .ForMember("ViewName", opt => opt.MapFrom(src => src.ViewName))
                 .ForMember("ParentId", opt => opt.MapFrom(src => src.ParentId))
                 .ForMember("Id", opt => opt.MapFrom(src => src.Id));
                cfg.CreateMap<AspNetUser, UserModel>()
                .ForMember("Login", opt => opt.MapFrom(src => src.UserName))
                .ForMember("FullName", opt => opt.MapFrom(src => src.UserFullName));                
            }).CreateMapper();
        }
        
        /// <summary>
        /// Получить пользователя по ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("User")]
        public async Task<AjaxOperationResult> GetUser(int id)
        {
            var item = await Service.GetUserByIdAsync(id);

            if (item == null)
            {
                return AjaxOperationResult.Error(_localizer["User not found"]);
            }

            var result = _mapper.Map<UserModel>(item);
            result.Password = "******";

            return AjaxOperationResult.Success(result);
        }

        /// <summary>
        /// Найти пользователя в каталоге Ldap
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpGet("FindLdapUser")]
        public async Task<AjaxOperationResult> GetUser(string login)
        {
            try
            {
                var ldapUserList = _ldap.Search(login);
                return AjaxOperationResult.Success(ldapUserList);
            }
            catch(Exception ex)
            {
                return AjaxOperationResult.Error("", ex.Message);
            }            
        }

        /// <summary>
        /// Добавить пользователя
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("User")]
        public async Task<AjaxOperationResult> PostUser(UserPostModel item)
        {
            if (ModelState.IsValid)
            {
                if(item.Provider == Model.Enums.Provider.Ldap)
                {
                    try
                    {
                        var ldapUserList = _ldap.Search(item.Login);
                        if (ldapUserList.Count == 0)
                        {
                            ModelState.AddModelError("Login", _localizer["User not Found in Ldap"]);
                            return AjaxOperationResult.Error(ModelState);
                        }
                    }
                    catch(Exception ex)
                    {
                        ModelState.AddModelError("Login", _localizer["User not Found in Ldap"]);
                        return AjaxOperationResult.Error(ModelState);
                    }                   
                }
                try
                {
                    PasswordHasher hashPass = new PasswordHasher();
                    item.Password = hashPass.HashPassword(item.Password);

                    var baseResult = await Service.CreateUserAsync(item.Login, item.Password, item.FullName, item.Email, item.Post, item.Provider);
                    var result = _mapper.Map<UserModel>(baseResult);
                    result.Password = "******";

                    return AjaxOperationResult.Success(result);
                }
                catch (ValidationErrors propertyErrors)
                {
                    ModelState.AddValidationErrors(propertyErrors);
                }
            }
            
            return AjaxOperationResult.Error(ModelState);
        }


        /// <summary>
        /// Обновить пользователя
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPut("User")]
        public async Task<AjaxOperationResult> PutUser(UserPutModel item)
        {
            if (ModelState.IsValid)
            {
                //TODO Перенести проверки в сервисы после переноса авторизации.
                var user = await Service.GetUserByIdAsync(item.Id);
                if (user == null)
                {
                    ModelState.AddModelError("Id", _localizer["User not found"]);
                    return AjaxOperationResult.Error(ModelState);
                }

                if (item.Password == "******")
                {
                    item.Password = user.PasswordHash;
                }
                else
                {
                    PasswordHasher hashPass = new PasswordHasher();
                    item.Password = hashPass.HashPassword(item.Password);
                }
                
                try
                {
                    user.UserName = item.Login;
                    user.Email = item.Email;
                    user.PasswordHash = item.Password;
                    user.NormalizedEmail = item.Email.ToUpper();
                    user.NormalizedUserName = item.Login.ToUpper();
                    user.UserFullName = item.FullName;
                    user.Post = item.Post;
                    var baseResult = await Service.UpdateUserAsync(user);
                    var result = _mapper.Map<UserModel>(baseResult);
                    result.Password = "******";

                    return AjaxOperationResult.Success(result);
                }
                catch (ValidationErrors propertyErrors)
                {
                    ModelState.AddValidationErrors(propertyErrors);
                }
            }

            return AjaxOperationResult.Error(ModelState);
        }
        /// <summary>
        /// Удалить пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("User")]
        public async Task<AjaxOperationResult> DeleteUser(int id)
        {
            try
            {
                await Service.DeleteUserAsync(id);

                return AjaxOperationResult.Success();
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

        /// <summary>
        /// получить все роли пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("UserRoles")]
        public async Task<AjaxOperationResult> GetUserRoles(int userId)
        {
            try
            {
                var roles = (await Service.GetUserRolesAsync(userId)).Select(c=> new RoleViewModel { 
                     Id = c.Id,
                     Name = c.Name,
                     ParentId = c.ParentId,
                     ViewName = c.ViewName
                });
               
                return AjaxOperationResult.Success(roles);
            }
            catch(Exception ex)
            {
                return AjaxOperationResult.Error(ex.Message);
            }
        }

        /// <summary>
        /// Обновить роли пользователя
        /// </summary>
        /// <param name="userRoles"></param>
        /// <returns></returns>
        [HttpPost("UserRoles")]
        public async Task<AjaxOperationResult> PostUserRoles(int userId, List<int> userRoles)
        {
            try
            {
                await Service.ChangeUserRoles(userId, userRoles);

                return AjaxOperationResult.Success();
            }
            catch (Exception ex)
            {
                return AjaxOperationResult.Error(ex.Message);
            }
        }
    }
}
