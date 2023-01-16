using AppApi.DTOs;
using AppApi.Entities;
using AppApi.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AppApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class BillsController : ControllerBase
    {
        private readonly IBillsRepository _billsRepository;
        private readonly IMapper _mapper;

        public BillsController(IBillsRepository billsRepository, IMapper mapper)
        {
            _mapper = mapper;
            _billsRepository = billsRepository;          
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserBill>>> GetBills()
        {
            var bills = await _billsRepository.GetBillsAsync();

            return Ok(_mapper.Map<IEnumerable<UserBillToReturnDto>>(bills));
        }



    }
}