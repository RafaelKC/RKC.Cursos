using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RKC.Cursos.Users;

namespace RKC.Cursos.Authentications
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;

        public AuthenticationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public string GenerateToken(User user)
        {
            var keyByte = Encoding.ASCII.GetBytes(_configuration.GetSection("CursosSettings").GetSection("AuthenticationKey").Value);
            var expirationTimeInMinutes = _configuration.GetSection("CursosSettings").GetSection("authenticationTimeoutInMinutes").Value;
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescripotor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.Name, user.UserName),
                    new("UserId", user.Id.ToString()),
                    new(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(int.Parse(expirationTimeInMinutes)),
                SigningCredentials = new SigningCredentials( new SymmetricSecurityKey(keyByte), SecurityAlgorithms.HmacSha256Signature),
                Issuer = "RKC.Cursos"
            };
            
            var token = tokenHandler.CreateToken(tokenDescripotor);
            return tokenHandler.WriteToken(token);
        }
    }
}