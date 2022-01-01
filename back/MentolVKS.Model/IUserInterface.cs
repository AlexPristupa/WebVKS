using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model
{
    /// <summary>
    /// Интерфейс для передачи имя пользователя в в сервисы.
    /// Для логирования  в БД   
    /// </summary>
    public interface IUserInterface
    {
        string GetUserName();
        string GetUserIp();
    }
}
