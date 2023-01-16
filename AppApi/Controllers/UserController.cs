using AppApi.DTOs;
using AppApi.Entities;
using AppApi.Errors;
using AppApi.Extensions;
using AppApi.Helpers;
using AppApi.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppApi.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<UserToReturnDto>> GetUser()
        {
            var username = User.GetUsername();
            var user = await _userRepository.GetUserAsync(username);

            return Ok(_mapper.Map<UserToReturnDto>(user));
        }

        [HttpGet("bills", Name = "GetUserBills")]
        public async Task<ActionResult<IEnumerable<UserBillToReturnDto>>> GetUserBills([FromQuery] UserParams userParams)
        {
            userParams.Username = User.GetUsername();

            var userBills = await _userRepository.GetUserBillsAsync(userParams);

            if (userBills == null) return NotFound(new ErrorResponse(404));

            Response.AddPaginationHeader(new PaginationHeader(userBills.PageNumber, userBills.PageSize, userBills.TotalCount, userBills.TotalPages));
            return Ok(userBills);
        }

        [HttpPost("bills")]
        public async Task<ActionResult<UserBillToReturnDto>> CreateBillRequest(UserBillToCreateDto userBillToCreate)
        {
            var username = User.GetUsername();
            var user = await _userRepository.GetUserWithBillsAsync(username);

            var date = DateTime.Now.AddMonths(-1);
            var validMonth = date.Month;
            var validYear = date.Year;

            if ((int)userBillToCreate.Month != validMonth || userBillToCreate.Year != validYear)
            {
                return BadRequest(new ErrorResponse(400, "Λάθος καταχώρηση περιόδου λογαριασμού"));
            }

            if (userBillToCreate.Ammount <= 0)
            {
                return BadRequest(new ErrorResponse(400, "Όχι επαρκές ποσό λογαριασμού προς επιδότηση"));
            }

            var userBill = _mapper.Map<UserBill>(userBillToCreate);

            user.UserBills.Add(userBill);

            if (await _userRepository.SaveAllAsync())
            {
                return Ok(_mapper.Map<UserBillToReturnDto>(userBill));
            }

            return BadRequest(new ErrorResponse(400, "Πρόβλημα κατά την δημιουργία της αίτησης"));
        }
    }
}