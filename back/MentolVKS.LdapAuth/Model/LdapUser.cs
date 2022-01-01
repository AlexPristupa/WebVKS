using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.LdapAuth.Model
{
    public class LdapUser
    {
        public string Login { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
    }
}
