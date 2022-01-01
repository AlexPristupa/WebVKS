using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MentolVKS.Auth
{
    public class AuthOptions
    {
        public const string IsUser = "MentolVKS"; // издатель токена
        public const string Audience = "http://localhost:51884/"; // потребитель токена
        const string Key = "mysupersecret_secretkey!123";   // ключ для шифрации
        public const int LifeTime = 60; // время жизни токена - 1 минута
        
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }

        private static ClaimsIdentity GetClaims(string login, List<string> roles)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimsIdentity.DefaultNameClaimType, login));

            foreach (var item in roles)
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, item));
            }

            ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }

        public static string GenerateToken(string login, List<string> roles, int IdleTimeOutMinute)
        {
            var claimsIdentity = GetClaims(login, roles);
            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: IsUser,
                audience: Audience,
                notBefore: now,
                claims: claimsIdentity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(IdleTimeOutMinute)),
                signingCredentials: new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }
    }
}
