using AutoMapper;
using BankApi.DTOs;
using BankApi.Entities;
using BankApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        public AccountsController(IAccountRepository accountRepository, IMapper mapper)
        {
            _mapper = mapper;
            _accountRepository = accountRepository;            
        }

        [HttpGet("{iban}")]
        public async Task<ActionResult<AccountDto>> GetAccount(string iban, [FromQuery] bool includeTransactions = false)
        {
            var account = await _accountRepository.GetAccountByIBANAsync(iban, includeTransactions);

            if (account == null) return NotFound();

            return Ok(_mapper.Map<AccountDto>(account));
        }

        [HttpPost("transactions/{iban}")]
        public async Task<ActionResult> PostTransaction(string iban, TransactionForCreationDto transaction)
        {
            var account = await _accountRepository.GetAccountByIBANAsync(iban, true);

            if (account == null) return NotFound();

            if (transaction.TransactionType == TransactionTypes.Credit)
            {
                account.Balance += transaction.Amount;
            }

            if (transaction.TransactionType == TransactionTypes.Debit)
            {
                if (account.Balance < transaction.Amount)
                {
                    return BadRequest("Account balance is not sufficient for the transaction");
                }

                account.Balance -= transaction.Amount;
            }

            var transactionEntity = new Transaction();
            _mapper.Map(transaction, transactionEntity);

            account.Transactions.Add(transactionEntity);

            if (await _accountRepository.SaveAllAsync()) return Ok();

            return BadRequest("Transaction was unsuccesfull");
        }

    }
}