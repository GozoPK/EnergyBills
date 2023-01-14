using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TaxisNetSimApi.DTOs;
using TaxisNetSimApi.Services;

namespace TaxisNetSimApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public AccountController(IUserRepository userRepository, IMapper mapper, IConfiguration config)
        {
            _config = config;
            _mapper = mapper;
            _userRepository = userRepository;           
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<UserToReturnDto>> GetUser()
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            var user = await _userRepository.GetUserByUsernameAsync(username);

            if (user == null) return NotFound();

            return Ok(_mapper.Map<UserToReturnDto>(user));
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserToReturnFromLoginDto>> Login(UserForLoginDto userForLogin)
        {
            var user = await _userRepository.GetUserByUsernameAsync(userForLogin.Username);

            if (user == null) return Unauthorized("Username or Password is not correct");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userForLogin.Password));

            for (int i=0; i<computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Username or Password is not correct");
            }

           var userToReturn = _mapper.Map<UserToReturnFromLoginDto>(user);
           userToReturn.TaxisnetToken = CreateToken(userToReturn.Username);

           return Ok(userToReturn);
        }

        private string CreateToken(string username)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]));

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, username)
            };

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(30),
                Issuer = _config["Token:Issuer"],
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

    }
}