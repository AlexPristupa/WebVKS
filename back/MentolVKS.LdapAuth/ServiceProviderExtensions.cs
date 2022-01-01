using Microsoft.Extensions.DependencyInjection;
using System;

namespace MentolVKS.LdapAuth
{
    public static class ServiceProviderExtensions
    {
        public static void AddLdapAuthService(this IServiceCollection services)
        {
            services.AddTransient<ILdapAuthInterface, LdapAuth>();
        }
    }
}
