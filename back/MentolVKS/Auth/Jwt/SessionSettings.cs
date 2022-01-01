using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentolVKS.Auth.Jwt
{
    public class SessionSettings
    {
        public int IdleTimeOutMinute { get; set; } = 5;
        public int IdleRefreshMinute { get; set; } = 60;
    }
}
