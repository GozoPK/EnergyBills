using AppApi.DTOs;
using AppApi.Entities;
using AppApi.Helpers;

namespace AppApi.Interfaces
{
    public interface IUserRepository
    {
        Task<PagedList<UserBillToReturnDto>> GetUserBillsAsync(UserParams userParams);
        Task<UserEntity> GetUserAsync(string username);
        Task<UserEntity> GetUserWithBillsAsync(string username);
        Task<bool> SaveAllAsync();
    }
}