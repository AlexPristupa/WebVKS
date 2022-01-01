using MentolVKS.Model.Auth;
using MentolVKS.Model.BaseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MentolVKS.Auth
{
    public class AllowedRolesAttribute : TypeFilterAttribute
    {
        public AllowedRolesAttribute(params Role[] roles)
            : base(typeof(RequiresPermissionAttributeExecutor))
        {
            var allowedRoles = roles.Select(role => Enum.GetName(typeof(Role), role)).ToList();
            Arguments = new object[] { new AuthorizationRequirement(allowedRoles) };
        }

        private class RequiresPermissionAttributeExecutor : Attribute, IAsyncResourceFilter
        {
            private readonly AuthorizationRequirement _requiredPermissions;
            private readonly RoleOptions _roleOptions;

            public RequiresPermissionAttributeExecutor(AuthorizationRequirement requiredPermissions, IOptions<RoleOptions> roleOptions)
            {
                _requiredPermissions = requiredPermissions;
                _roleOptions = roleOptions.Value;
            }

            public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
            {
                var principal = new ClaimsPrincipal(context.HttpContext.User.Identity);
                if (principal.Identity?.Name == null)
                {
                    context.Result = new UnauthorizedResult();
                    await context.Result.ExecuteResultAsync(context);
                    return;
                }

                var isValidRole = false;               

                foreach (var item in _requiredPermissions.RequiredPermissions)
                {                   
                    if (principal.Claims.FirstOrDefault(c=>c.Value==item)!=null) //IsInRole(item))
                    {
                        isValidRole = true;
                        break;
                    }

                    var childRoles = new List<int>();
                    var parentRoles = new List<int?>();

                    _roleOptions.Clear();

                    if (Enum.TryParse(item, out Role role))
                    {
                        var value = (int)role;
                        childRoles = _roleOptions.GetChilds(value).ToList();
                        parentRoles = _roleOptions.GetParents(value).Where(p => p != null).ToList();
                    }
                    else
                    {
                        isValidRole = false;
                    }

                    if (childRoles.Count <= 0) continue;

                    foreach (var child in childRoles)
                    {
                        var r = (Role)child;

                        if (principal.Claims.FirstOrDefault(c => c.Value == r.ToString()) != null) continue;
                        
                        //if (!principal.IsInRole(r.ToString())) continue;

                        isValidRole = true;
                        break;
                    }
                }

                if (isValidRole == false)
                {
                    context.Result = new UnauthorizedResult();
                    await context.Result.ExecuteResultAsync(context);
                }
                else
                {
                    await next();
                }
            }
        }
    }
}
