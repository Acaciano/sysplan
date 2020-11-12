using Sysplan.Application.Interfaces;
using Sysplan.Application.ViewModels;
using Sysplan.Application.ViewModels.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sysplan.Application.Services.Token
{
    public class TokenServiceApp : ITokenServiceApp
    {
        private readonly IConfiguration _configuration;
        public TokenServiceApp(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TokenViewModel GenereteToken(ClienteViewModel usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWT:ApiSecret"));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("idUser", usuario.Id.ToString()),
                    new Claim("name", usuario.Nome.ToString()),
                    new Claim("email", usuario.Email.ToString())
                }),

                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };

            var createToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(createToken);

            return new TokenViewModel(token, tokenDescriptor.Expires.Value);

        }
    }
}
