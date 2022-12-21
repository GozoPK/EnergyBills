using Microsoft.EntityFrameworkCore;
using TaxisNetSimApi.Data;
using TaxisNetSimApi.Entities;

namespace TaxisNetSimApi.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;
        public UserRepository(UserContext context)
        {
            _context = context;            
        }

        public async Task<TaxisNetUserEntity> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Username == username);
        }
    }
}