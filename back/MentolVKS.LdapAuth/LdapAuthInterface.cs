using MentolVKS.LdapAuth.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.LdapAuth
{
    public interface ILdapAuthInterface
    {
        /// <summary>
        /// Проверка имени и пароля по домену
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        LdapUser Login(string login, string password);
        /// <summary>
        /// Поиск пользователя по домену формат login@domain
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        List<LdapUser> Search(string search);
        /// <summary>
        /// Поиск пользователя по домену 
        /// </summary>
        /// <param name="search">имя пользователя</param>
        /// <param name="domain">домен</param>
        /// <returns></returns>
        List<LdapUser> Search(string search, string domain);
        /// <summary>
        /// Домен по умолчанию
        /// </summary>
        /// <returns></returns>
        string GetDefaulDomain();
        /// <summary>
        /// Кол-во доменов
        /// </summary>
        /// <returns></returns>
        public int LdapConfigCount();
    }
}
