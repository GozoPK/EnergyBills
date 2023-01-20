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

            var checkExistingNumber = user.UserBills.Any(bill => bill.BillNumber == userBillToCreate.BillNumber);
            if (checkExistingNumber)
            {
                return BadRequest(new ErrorResponse(400, "Ο αριθμός λογαριασμού υπάρχει ήδη"));
            }

            if (userBillToCreate.State == State.Submitted)
            {
                var validationsMessage = ApplyValidations(userBillToCreate, user);
                if (!string.IsNullOrEmpty(validationsMessage))
                {
                    return BadRequest(new ErrorResponse(400, validationsMessage));
                }
            }

            var userBill = _mapper.Map<UserBill>(userBillToCreate);

            user.UserBills.Add(userBill);

            if (await _userRepository.SaveAllAsync())
            {
                return Ok(_mapper.Map<UserBillToReturnDto>(userBill));
            }

            return BadRequest(new ErrorResponse(400, "Πρόβλημα κατά την δημιουργία της αίτησης"));
        }

        [HttpPut("bills")]
        public async Task<ActionResult<UserBillToReturnDto>> UpdateUserBill(UserBillToCreateDto userBillToCreate)
        {
            var username = User.GetUsername();
            var user = await _userRepository.GetUserWithBillsAsync(username);

            var userBillForUpdate = user.UserBills.FirstOrDefault(bill => bill.BillNumber == userBillToCreate.BillNumber);
            if (userBillForUpdate == null)
            {
                return BadRequest(new ErrorResponse(400, "Δεν βρέθηκε αίτηση για αυτον τον αριθμό λογαριασμού"));
            }

            if (userBillToCreate.State == State.Submitted)
            {
                var validationsMessage = ApplyValidations(userBillToCreate, user);
                if (!string.IsNullOrEmpty(validationsMessage))
                {
                    return BadRequest(new ErrorResponse(400, validationsMessage));
                }
            }

            _mapper.Map(userBillToCreate, userBillForUpdate);
            if (await _userRepository.SaveAllAsync())
            {
                return Ok(_mapper.Map<UserBillToReturnDto>(userBillForUpdate));
            }

            return BadRequest(new ErrorResponse(400, "Πρόβλημα κατά την αποθήκευση της αίτησης"));
        }

        private string ApplyValidations(UserBillToCreateDto userBillToCreate, UserEntity user)
        {
            var date = DateTime.Now.AddMonths(-1);
            var validMonth = date.Month;
            var validYear = date.Year;

            if ((int)userBillToCreate.Month != validMonth || userBillToCreate.Year != validYear)
            {
                return "Λάθος καταχώρηση περιόδου λογαριασμού";
            }

            if (userBillToCreate.Ammount <= 0)
            {
                return "Όχι επαρκές ποσό λογαριασμού προς επιδότηση";
            }

            var userBillForCheck = user.UserBills.FirstOrDefault(bill => bill.Month == userBillToCreate.Month && bill.Year == userBillToCreate.Year);
            if (userBillForCheck != null)
            {
                if (userBillForCheck.Type == BillType.Both)
                {
                    return "Έχετε ήδη υποβάλει αίτηση για αυτό το μήνα";
                } 
                    
                if (userBillForCheck.Type == userBillToCreate.Type || userBillToCreate.Type == BillType.Both)
                {
                    return "Έχετε ήδη υποβάλει αίτηση για αυτό το τύπο λογαριασμού";
                }
            }

            return null;
        }
    }
}