using MentolVKS.Common;
using MentolVKS.LdapAuth.Model;
using Microsoft.Extensions.Options;
using Novell.Directory.Ldap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MentolVKS.LdapAuth
{
    public class LdapAuth : ILdapAuthInterface
    {
        private LdapConnection _ldapConnection;
        private readonly List<LdapConfig> _config;
        /// <summary>
        /// Инициализируем подключение в конструкторе с LDAP сервером
        /// </summary>        
        public LdapAuth(IOptions<List<LdapConfig>> config)
        {
            _config = config.Value;
        }
         
        
        private string GetLogin(string item)
        {
            var splitLogin = item.Split('@');
            if (splitLogin.Length != 2)
                return String.Empty;

            return splitLogin[0];
        }

        private string GetDomain(string item)
        {
            var splitLogin = item.Split('@');
            if (splitLogin.Length != 2)
                return String.Empty;
            
            return splitLogin[1];
        }

        /// <summary>
        /// Получить конфиг для домена
        /// </summary>
        /// <param name="login"></param>
        private LdapConfig GetLdapConfigItemByLogin(string login)
        {
            return GetLdapConfigItemByDomain(GetDomain(login));         
        }

        private LdapConfig GetLdapConfigItemByDomain(string domain)
        {
            return _config.FirstOrDefault(c => c.Name.ToUpper() == domain.ToUpper());
        }

        public LdapUser Login(string login, string password)
        {
            var currentConfig = GetLdapConfigItemByLogin(login);
            if (currentConfig == null)
                return null;

            LdapUser user = new LdapUser();

            using (var connection = new LdapConnection())
            {
                connection.Connect(currentConfig.Url, currentConfig.Port);
                connection.Bind(currentConfig.BindDn, CryptoManager.AesDecrypt(currentConfig.BindCredentials));

                var searchFilter = string.Format(currentConfig.SearchFilter, GetLogin(login));


                var result = connection.Search(
                    currentConfig.SearchBase,
                    LdapConnection.ScopeSub,
                    searchFilter,
                    new[] { "displayName", "sAMAccountName","mail" },
                    false
                );

                var bufUser = result.Next();

                if (bufUser != null)
                {
                    connection.Bind(bufUser.Dn, password);
                    if (connection.Bound)
                    {
                        user.Login = bufUser.GetAttribute("sAMAccountName").StringValue;
                        user.DisplayName = bufUser.GetAttribute("displayName").StringValue;
                        user.Email = bufUser.GetAttribute("mail").StringValue;
                    }
                }

                return null;
            }
        }

        public List<LdapUser> Search(string search)
        {
            var login = GetLogin(search);
            var domain = GetDomain(search);
            return Search(login, domain);
        }
        public List<LdapUser> Search(string search, string domain)
        {
            List<LdapUser> users = new List<LdapUser>();

            var currentConfig = GetLdapConfigItemByDomain(domain);
            if (currentConfig == null)
                return users;

            using (var connection = new LdapConnection())
            {
                connection.Connect(currentConfig.Url, currentConfig.Port);
                connection.Bind(currentConfig.BindDn, CryptoManager.AesDecrypt(currentConfig.BindCredentials));

                var result = connection.Search(
                                currentConfig.SearchBase,
                                LdapConnection.ScopeSub,
                                "(objectclass=*)",
                                new[] { "displayName", "sAMAccountName", "mail" },
                                false
                                );


                while (result.HasMore())
                {
                    var ldapUser = result.Next();
                    var login = "";
                    var displayName = "";
                    var email = "";

                    try
                    {
                        login = ldapUser.GetAttribute("sAMAccountName").StringValue;
                        displayName = ldapUser.GetAttribute("displayName").StringValue;
                        email = ldapUser.GetAttribute("mail").StringValue;
                    }
                    catch (Exception ex)
                    {

                    }
                    Console.WriteLine(login);
                    Console.WriteLine(login.ToLower().Contains(search.ToLower()));
                    Console.WriteLine(displayName);
                    Console.WriteLine(displayName.ToLower().Contains(search.ToLower()));
                    if (login.ToLower().Contains(search.ToLower()) || displayName.ToLower().Contains(search.ToLower()))
                    {
                        users.Add(new LdapUser
                        {
                            Login = login+"@"+domain,
                            DisplayName = displayName,
                            Email = email
                        });
                    }
                }
            }
            return users;
        }

        public string GetDefaulDomain()
        {
            if (_config.Any())
                return _config[0].Name;

            return String.Empty;
        }

        public int LdapConfigCount()
        {
            if (_config.Any())
                return _config.Count;

            return 0;
        }
    }
}
