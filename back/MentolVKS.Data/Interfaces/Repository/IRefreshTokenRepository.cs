using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MentolVKS.Data.Interfaces.Repository
{
    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {
        /// <summary>
        /// Поиск токена
        /// </summary>
        /// <param name="token"></param>
        /// <param name="fingerPrint"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task<RefreshToken> SearchRefreshTokenAsync(string token, string fingerPrint, string ipAddress);        
    }
}
