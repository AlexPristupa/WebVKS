using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentolVKS.Auth
{
    public class AuthorizationRequirement : IAuthorizationRequirement
    {
        public IEnumerable<string> RequiredPermissions { get; }

        public AuthorizationRequirement(IEnumerable<string> requiredPermissions)
        {
            RequiredPermissions = requiredPermissions;
        }
    }
}
