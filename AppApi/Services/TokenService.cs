using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AppApi.Data;
using AppApi.DTOs;
using AppApi.Entities;
using Microsoft.IdentityModel.Tokens;

namespace AppApi.Services
{
    public class TokenService : ITokenService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config, DataContext context)
        {
            _config = config;
            _context = context;
        }

        public string CreateToken(AccountToReturnDto account)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["AppTokenKey"]));

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.NameId, account.Username),
                new Claim(JwtRegisteredClaimNames.UniqueName, account.Afm)
            };

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(3),
                SigningCredentials = credentials,
                IssuedAt = DateTime.Now
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string CreateToken(TaxisnetUserDto user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["RegisterTokenKey"]));

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Username),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Afm)
            };

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(30),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}