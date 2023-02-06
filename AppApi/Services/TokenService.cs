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

        public string CreateToken(UserToReturnDto account)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["AppToken:Key"]));

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, account.Username),
                new Claim(ClaimTypes.Email, account.Email),
                new Claim(ClaimTypes.Role, account.Role)
            };

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(3),
                SigningCredentials = credentials,
                Issuer = _config["AppToken:Issuer"],
                IssuedAt = DateTime.Now
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string CreateToken(TaxisnetUserDto user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["RegisterToken:Key"]));

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(30),
                Issuer = _config["RegisterToken:Issuer"],
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}