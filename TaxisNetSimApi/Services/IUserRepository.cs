using TaxisNetSimApi.Entities;

namespace TaxisNetSimApi.Services
{
    public interface IUserRepository
    {
        Task<TaxisNetUserEntity> GetUserByUsernameAsync(string username);
    }
}