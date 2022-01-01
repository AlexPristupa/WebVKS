using MentolVKS.Data.Interfaces;
using MentolVKS.LdapAuth;
using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using MentolVKS.Service.Contract;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MentolVKS.Service
{
    public partial class Service : IService
    {
        public Service(IUnitOfWork unitOfWork, IStringLocalizer<Service> localizer, IUserInterface userName, ILdapAuthInterface ldap)
        {
            UnitOfWork = unitOfWork;
            Localizer = localizer;
            UserName = userName;
            Ldap = ldap;
        }

        protected IUnitOfWork UnitOfWork { get; }
        protected IStringLocalizer<Service> Localizer { get; }
        protected IUserInterface UserName { get; }
        protected ILdapAuthInterface Ldap { get; }
    }
}