using BankApi.Entities;

namespace BankApi.Services
{
    public interface IAccountRepository
    {
        Task<Account> GetAccountByIBANAsync(string iban);
        Task<Account> GetAccountByIBANAsync(string iban, bool includeTransactions);
        Task<bool> SaveAllAsync();
    }
}