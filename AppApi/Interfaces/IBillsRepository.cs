using AppApi.DTOs;
using AppApi.Entities;
using AppApi.Helpers;

namespace AppApi.Services
{
    public interface IBillsRepository
    {
        Task<PagedList<UserBillToReturnDto>> GetBillsAsync(UserParams userParams);
        Task<UserBill> GetBillByIdAsync(string id);
        Task<bool> SaveAllAsync();
    }
}