using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.LdapAuth.Model
{
    public class LdapConfig
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public int Port { get; set; }
        public string BindDn { get; set; }
        public string BindCredentials { get; set; }
        public string SearchBase { get; set; }
        public string SearchFilter { get; set; }
        public string AdminCn { get; set; }
    }
}
