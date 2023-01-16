using AppApi.DTOs;
using AppApi.Entities;

namespace AppApi.Services
{
    public interface IBillsRepository
    {
        Task<IEnumerable<UserBill>> GetBillsAsync();
        Task UpdateBillRequest(string username, UserBillToCreateDto userBillToCreate);
        Task<bool> SaveAllAsync();
    }
}