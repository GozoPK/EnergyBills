using AppApi.Entities;

namespace AppApi.Services
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserEntity>> GetUsersAsync();
        Task<UserEntity> GetUserByUsernameAsync(string username);
        Task<UserEntity> GetUserByAfmAsync(string afm);
        Task AddUser(UserEntity user);
        Task<bool> UserExists(string username, string afm, string email);
        Task<bool> SaveAllAsync();
    }
}