using System.Security.Cryptography;
using System.Text;
using AppApi.DTOs;
using AppApi.Entities;
using AppApi.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IHttpService _httpService;
        private readonly ITokenService _tokenService;
        public AccountController(IUserRepository userRepository ,IMapper mapper, IHttpService httpService,
            ITokenService tokenService)
        {
            _tokenService = tokenService;
            _httpService = httpService;
            _userRepository = userRepository;
            _mapper = mapper;            
        }

        [Authorize(AuthenticationSchemes = "register")]
        [HttpPost("register")]
        public async Task<ActionResult<AccountToReturnDto>> Register(UserForRegisterDto userForRegister)
        {
            if ( await _userRepository.UserExists(userForRegister.Username, userForRegister.Afm)) return BadRequest("User already exists");

            var user = _mapper.Map<UserEntity>(userForRegister);

            user.Id = Guid.NewGuid();

            using var hmac = new HMACSHA512();

            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userForRegister.Password));
            user.PasswordSalt = hmac.Key;

            await _userRepository.AddUser(user);
            await _userRepository.SaveAllAsync();

            var account = _mapper.Map<AccountToReturnDto>(user);
            account.Token = _tokenService.CreateToken(account);
            return Ok(account);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AccountToReturnDto>> Login(UserForLoginDto login)
        {
            var user = await _userRepository.GetUserByUsernameAsync(login.Username);

            if (user == null) 
            {
                return Unauthorized("Invalid username or password");
            }

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));

            for (int i=0; i<computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid username or password");
            }

            var account = _mapper.Map<AccountToReturnDto>(user);
            account.Token = _tokenService.CreateToken(account);

            return Ok(account);
        }

        [HttpPost("taxisnet-login")]
        public async Task<ActionResult<TaxisnetUserDto>> TaxisnetLogin(UserForLoginDto user)
        {
            var taxisnetUser = await _httpService.TaxisnetLogin(user);

            if (taxisnetUser == null)
            {
                return Unauthorized("Invalid Username or Password");
            }

            taxisnetUser.Token = _tokenService.CreateToken(taxisnetUser);

            return Ok(taxisnetUser);            
        }

    }
}