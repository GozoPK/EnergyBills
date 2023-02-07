using AppApi.DTOs;
using AppApi.Entities;
using AppApi.Errors;
using AppApi.Extensions;
using AppApi.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHttpService _httpService;
        private readonly ITokenService _tokenService;
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly RoleManager<UserRole> _roleManager;
        public AccountController(UserManager<UserEntity> userManager, IMapper mapper, IHttpService httpService,
            SignInManager<UserEntity> signInManager, ITokenService tokenService, RoleManager<UserRole> roleManager)
        {
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
            _httpService = httpService;
            _mapper = mapper;            
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<ActionResult<UserToReturnDto>> GetCurrentUser()
        {
            var username = User.GetUsername();

            var user =  await _userManager.FindByNameWithRoleAsync(username);

            return Ok(_mapper.Map<UserToReturnDto>(user));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        public async Task<ActionResult<UserToReturnDto>> EditUser(UserForUpdateDto userForUpdate)
        {
            var username = User.GetUsername();

            var user =  await _userManager.FindByNameWithRoleAsync(username);

            _mapper.Map(userForUpdate, user);

           var result = await  _userManager.UpdateAsync(user);
           if (result.Succeeded)
           {
                return Ok(_mapper.Map<UserToReturnDto>(user));
           }

           return BadRequest(new ErrorResponse(400, "Πρόβλημα στην αποθήκευση χρήστη"));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("change-password")]
        public async Task<ActionResult> ChangePassword(ChangePasswordDto changePassword)
        {
            var username = User.GetUsername();

            var user =  await _userManager.FindByNameAsync(username);

            if (changePassword.OldPassword == changePassword.NewPassword)
            {
                return BadRequest(new ErrorResponse(400, "Ο νέος κωδικός είναι ίδιος με τον παλιό"));
            }

            var result = await _userManager.ChangePasswordAsync(user, changePassword.OldPassword, changePassword.NewPassword);
            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(new ErrorResponse(400, "Πρόβλημα κατά την αλλαγή κωδικού"));
        }

        [Authorize(AuthenticationSchemes = "register")]
        [HttpPost("register")]
        public async Task<ActionResult<UserToReturnDto>> Register(UserForRegisterDto userForRegister)
        {
            try
            {
                if ( await UserExists(userForRegister))
                {
                    return BadRequest(new ErrorResponse(400, "Έχετε πραγματοποιήσει ήδη εγγραφή για αυτόν το χρήστη"));
                } 

                var userNameFromTaxis = User.GetUsername();
                var taxisnetUser = await _httpService.GetUser(userForRegister.TaxisnetToken);

                if (CheckIfInvalidUser(taxisnetUser, userForRegister))
                {
                    return BadRequest(new ErrorResponse(400));
                }

                var user = _mapper.Map<UserEntity>(userForRegister);

                var role = await _roleManager.FindByNameAsync("Member");
                user.RoleId = role.Id;

                var result = await _userManager.CreateAsync(user, userForRegister.Password);

                if (!result.Succeeded) return BadRequest(new ErrorResponse(400));

                var roleResult = await _userManager.AddToRoleAsync(user, "Member");

                if (!roleResult.Succeeded) return BadRequest(new ErrorResponse(400));

                var account = _mapper.Map<UserToReturnDto>(user);
                account.Role = role.Name;
                account.Token = _tokenService.CreateToken(account);
                return Ok(account);
            }
            catch (StatusCodeException ex)
            {
                return new ObjectResult(new ErrorResponse(ex.Error.StatusCode));
            }
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthenticationErrorResponse), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserToReturnDto>> Login(UserForLoginDto login)
        {
            var user = await _userManager.FindByNameWithRoleAsync(login.Username);

            if (user == null) 
            {
                return Unauthorized(new AuthenticationErrorResponse(401, "Λανθασμένο όνομα χρήστη ή κωδικού."));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);

            if (!result.Succeeded) return Unauthorized(new AuthenticationErrorResponse(401, "Λανθασμένο όνομα χρήστη ή κωδικού."));

            var account = _mapper.Map<UserToReturnDto>(user);
            account.Token = _tokenService.CreateToken(account);

            return Ok(account);
        }

        [HttpPost("taxisnet-login")]
        [ProducesResponseType(typeof(AuthenticationErrorResponse), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<TaxisnetUserDto>> TaxisnetLogin(UserForLoginDto user)
        {
            try
            {
                var taxisnetUser = await _httpService.TaxisnetLogin(user);

                if (taxisnetUser == null)
                {
                    return Unauthorized(new AuthenticationErrorResponse(401, "Λανθασμένο όνομα χρήστη ή κωδικού."));
                }

                taxisnetUser.Token = _tokenService.CreateToken(taxisnetUser);

                return Ok(taxisnetUser);            
            }
            catch (StatusCodeException ex)
            {
                if (ex.Error.StatusCode == 401)
                {
                    return Unauthorized(new AuthenticationErrorResponse(401, "Λανθασμένο όνομα χρήστη ή κωδικού."));
                }
                
                return new ObjectResult(new ErrorResponse(ex.Error.StatusCode));
            }
        }

        private async Task<bool> UserExists(UserForRegisterDto userForRegister)
        {
            return ((await _userManager.Users.AnyAsync(user => user.UserName == userForRegister.Username))
                || (await _userManager.Users.AnyAsync(user => user.Email == userForRegister.Email))
                || (await _userManager.Users.AnyAsync(user => user.Afm == userForRegister.Afm)));
        }

        private bool CheckIfInvalidUser(TaxisnetUserDto taxisnetUser, UserForRegisterDto userForRegister)
        {
            return taxisnetUser.Afm != userForRegister.Afm ||
                taxisnetUser.FirstName != userForRegister.FirstName ||
                taxisnetUser.LastName != userForRegister.LastName ||
                taxisnetUser.AddressNumber != userForRegister.AddressNumber ||
                taxisnetUser.AddressStreet != userForRegister.AddressStreet ||
                taxisnetUser.AnnualIncome != userForRegister.AnnualIncome ||
                taxisnetUser.City != userForRegister.City ||
                taxisnetUser.PostalCode != userForRegister.PostalCode;
        }

    }
}