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
    [Route("api/user/bills")]
    public class UserBillsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserBillsRepository _userBillsRepository;
        public UserBillsController(IUserBillsRepository userBillsRepository, IMapper mapper)
        {
            _userBillsRepository = userBillsRepository;
            _mapper = mapper;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<UserBillToReturnDto>>> GetUserBills([FromQuery] UserParams userParams)
        {
            userParams.Username = User.GetUsername();

            var userBills = await _userBillsRepository.GetUserBillsAsync(userParams);

            if (userBills == null) return NotFound(new ErrorResponse(404));

            Response.AddPaginationHeader(new PaginationHeader(userBills.PageNumber, userBills.PageSize, userBills.TotalCount, userBills.TotalPages));
            return Ok(userBills);
        }

        [HttpPost()]
        public async Task<ActionResult<UserBillToReturnDto>> CreateBillRequest(UserBillToCreateDto userBillToCreate)
        {
            var username = User.GetUsername();
            var user = await _userBillsRepository.GetUserWithBillsAsync(username);

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

            if (await _userBillsRepository.SaveAllAsync())
            {
                return Ok(_mapper.Map<UserBillToReturnDto>(userBill));
            }

            return BadRequest(new ErrorResponse(400, "Πρόβλημα κατά την δημιουργία της αίτησης"));
        }

        [HttpPut()]
        public async Task<ActionResult<UserBillToReturnDto>> UpdateUserBill(UserBillToCreateDto userBillToCreate)
        {
            var username = User.GetUsername();
            var user = await _userBillsRepository.GetUserWithBillsAsync(username);

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
                userBillForUpdate.DateOfCreation = DateTime.Now;
            }

            _mapper.Map(userBillToCreate, userBillForUpdate);
            if (!_userBillsRepository.HasChanges())
            {
                return NoContent();
            }
            
            if (await _userBillsRepository.SaveAllAsync())
            {
                return Ok(_mapper.Map<UserBillToReturnDto>(userBillForUpdate));
            }

            return BadRequest(new ErrorResponse(400, "Πρόβλημα κατά την αποθήκευση της αίτησης"));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBill(string id)
        {
            var username = User.GetUsername();
            var user = await _userBillsRepository.GetUserWithBillsAsync(username);

            var billToDelete = user.UserBills.FirstOrDefault(bill => bill.Id.ToString() == id);

            if (billToDelete == null)
            {
                return BadRequest(new ErrorResponse(400, "Δεν βρέθηκε ο λογαριασμός"));
            }

            if (billToDelete.State == State.Submitted) 
            {
                return BadRequest(new ErrorResponse(400, "Π λογαριασμός έχει ήδη υποβληθεί. Η διαγραφή απέτυχε"));
            }

            user.UserBills.Remove(billToDelete);
            if (await _userBillsRepository.SaveAllAsync())
            {
                return Ok();
            }

            return BadRequest(new ErrorResponse(400, "Πρόβλημα κατά την διαγραφή της αίτησης")); 
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

            var userBillForCheck = user.UserBills.FirstOrDefault(bill => bill.Month == userBillToCreate.Month && bill.Year == userBillToCreate.Year
                && bill.State == State.Submitted);
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