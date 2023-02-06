using AppApi.DTOs;
using AppApi.Entities;
using AppApi.Errors;
using AppApi.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppApi.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "RequireAdminRole")]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<UserRole> _roleManager;
        private readonly IMapper _mapper;
        public AdminController(UserManager<UserEntity> userManager, RoleManager<UserRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserToReturnDto>>> GetAdminUsers()
        {
            var admins = await _userManager.GetAdminUsersAsync();
            return Ok(_mapper.Map<IEnumerable<UserToReturnDto>>(admins));
        }

        [HttpGet("{username}", Name = "GetAdminUser")]
        public async Task<ActionResult<UserToReturnDto>> GetAdminUser(string username)
        {
            var admin = await _userManager.FindByNameAsync(username);

            if (admin == null)
            {
                return BadRequest(new ErrorResponse(400, "Λάθος όνομα χρήστη"));
            }

            return Ok(_mapper.Map<UserToReturnDto>(admin));
        }

        [HttpPost]
        public async Task<ActionResult<UserToReturnDto>> CreateAdminUser(AdminForCreationDto adminForCreation)
        {
            if (await AdminExists(adminForCreation))
            {
                return BadRequest(new ErrorResponse(400, "Ο χρήστης υπάρχει ήδη"));
            }

            var admin = new UserEntity
            {
                UserName = adminForCreation.Username,
                Email = adminForCreation.Email
            };

            var role = await _roleManager.FindByNameAsync("Admin");
            admin.RoleId = role.Id;

            var result = await _userManager.CreateAsync(admin, adminForCreation.Password);

            if (!result.Succeeded) return BadRequest(new ErrorResponse(400));

            var roleResult = await _userManager.AddToRoleAsync(admin, "Admin");

            if (!roleResult.Succeeded) return BadRequest(new ErrorResponse(400));

            return CreatedAtRoute("GetAdminUser", new { username = adminForCreation.Username }, _mapper.Map<UserToReturnDto>(admin));
        }

        private async Task<bool> AdminExists(AdminForCreationDto adminForCreation)
        {
            return ((await _userManager.Users.AnyAsync(user => user.UserName == adminForCreation.Username))
                && (await _userManager.Users.AnyAsync(user => user.Email == adminForCreation.Email)));
        }
    }
}