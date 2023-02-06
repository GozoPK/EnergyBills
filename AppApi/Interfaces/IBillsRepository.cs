using AppApi.DTOs;
using AppApi.Entities;

namespace AppApi.Services
{
    public interface IBillsRepository
    {
        Task<IEnumerable<UserBill>> GetBillsAsync();
        Task<UserBill> GetBillByIdAsync(string id);
        Task<bool> SaveAllAsync();
    }
}