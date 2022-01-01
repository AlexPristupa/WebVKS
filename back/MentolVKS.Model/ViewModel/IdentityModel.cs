using System.Collections.Generic;

namespace MentolVKS.Model.ViewModel
{
    /// <summary>
    /// Модель пользователя.
    /// </summary>
    public class IdentityModel
    {
        /// <summary>
        /// Токен доступа.
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Логин.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string UserFullName { get; set; }

        /// <summary>
        /// Список ролей.
        /// </summary>
        public List<string> Roles { get; set; }

        /// <summary>
        /// Refresh Token.
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Время жизни токена.
        /// </summary>
        public int TimeOutMinute { get; set; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public int Id { get; set; }
    }
}
