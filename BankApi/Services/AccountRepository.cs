using BankApi.Data;
using BankApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankApi.Services
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankContext _context;
        public AccountRepository(BankContext context)
        {
            _context = context;            
        }

        public async Task<Account> GetAccountByIBANAsync(string iban)
        {
            return await _context.Accounts.FirstOrDefaultAsync(account => account.IBAN.ToLower() == iban.ToLower());
        }

        public async Task<Account> GetAccountByIBANAsync(string iban, bool includeTransactions)
        {
            if (!includeTransactions) return await this.GetAccountByIBANAsync(iban);

            return await _context.Accounts.Include(account => account.Transactions).FirstOrDefaultAsync(account => account.IBAN == iban);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}