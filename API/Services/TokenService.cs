using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;
namespace API.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        // our JWT is using symmetric key for encryption
        // this key is used for signing our JWT and signature
        // verification.
        public TokenService(IConfiguration config){
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                config["TokenKey"] // bu tokenkey appsettings.development.jsondan geliyor
            ));
        }
        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim>{
                new Claim(JwtRegisteredClaimNames.NameId,
                user.UserName)
            };

            var creds = new SigningCredentials(_key,
            SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}