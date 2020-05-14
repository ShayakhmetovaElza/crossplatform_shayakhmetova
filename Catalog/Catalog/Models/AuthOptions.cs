using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Catalog.Models
{
    public class AuthOptions
    {
        public static string ISSUER => "Server";
        public static string AUDIENCE => "APIclients";
        public static int LIFETIME => 1;
        public static SecurityKey SigningKey => new SymmetricSecurityKey(Encoding.ASCII.GetBytes("mysupersecretkey-123"));
        internal static string GenerateToken(bool is_admin = false)
        {
            var now = DateTime.UtcNow;

            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, "user"),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, is_admin?"admin":"guest")
                };
            ClaimsIdentity identity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: ISSUER,
                    audience: AUDIENCE,
                    notBefore: now,
                    expires: now.AddYears(LIFETIME),
                    claims: identity.Claims,
                    signingCredentials: new SigningCredentials(SigningKey, SecurityAlgorithms.HmacSha256)); ;
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;

        }
    }
}
