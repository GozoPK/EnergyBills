using AppApi.DTOs;
using AppApi.Entities;
using AppApi.Errors;
using AppApi.Helpers;
using AppApi.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AppApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "RequireAdminRole")]
    [ApiController]
    [Route("api/[controller]")]
    public class BillsController : ControllerBase
    {
        private readonly IBillsRepository _billsRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<UserEntity> _userManager;

        public BillsController(IBillsRepository billsRepository, UserManager<UserEntity> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
            _billsRepository = billsRepository;          
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserBillToReturnDto>>> GetBills()
        {
            var bills = await _billsRepository.GetBillsAsync();

            return Ok(_mapper.Map<IEnumerable<UserBillToReturnDto>>(bills));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BillInfoForAdminToReturnDto>> GetBill(string id)
        {
            var bill = await _billsRepository.GetBillByIdAsync(id);
            if (bill == null) return BadRequest(new ErrorResponse(400, "Ο λογαριασμός δεν βρέθηκε"));

            var billToReturn = _mapper.Map<BillInfoForAdminToReturnDto>(bill);

            return Ok(billToReturn);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBill(string id, BillStatusUpdateDto status)
        {
            var bill = await _billsRepository.GetBillByIdAsync(id);
            if (bill == null) return BadRequest(new ErrorResponse(400, "Ο λογαριασμός δεν βρέθηκε"));

            if (status.Status == Status.Rejected)
            {
                bill.Status = status.Status;
            }

            if (status.Status == Status.Approved)
            {
                bill.AmmountToReturn = CalculateSubsidy(bill.Ammount, bill.UserEntity.AnnualIncome);
                bill.Status = status.Status;
            }

            if (await _billsRepository.SaveAllAsync())
            {
                return Ok();
            }

            return BadRequest(new ErrorResponse(400, "Σφάλμα στην επεξεργασία της αίτησης"));
        }

        private decimal CalculateSubsidy(decimal amount, decimal income)
        {
            if (income <= (decimal)12000.00)
            {
                return amount * (decimal)0.25;
            }

            if (income <= (decimal)17000.00)
            {
                return amount * (decimal)0.15;
            }

            if (income <= (decimal)22000.00)
            {
                return amount * (decimal)0.08;
            }

            return 0;
        }
    }
}