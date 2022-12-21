using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

        public AccountController(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;           
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserToReturnDto>> Login(UserForLoginDto userForLogin)
        {
            var user = await _userRepository.GetUserByUsernameAsync(userForLogin.Username);

            if (user == null) return Unauthorized("Username or Password is not correct");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userForLogin.Password));

            for (int i=0; i<computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Username or Password is not correct");
            }

            return Ok(_mapper.Map<UserToReturnDto>(user));
        }

    }
}