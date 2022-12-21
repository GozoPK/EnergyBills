using AppApi.Data;
using AppApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppApi.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;            
        }

        public async Task<UserEntity> GetUserByAfmAsync(string afm)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Afm == afm);
        }

        public async Task<UserEntity> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Username == username);
        }

        public async Task<IEnumerable<UserEntity>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<bool> UserExists(string username, string afm)
        {
            return ((await _context.Users.AnyAsync(user => user.Username == username)) && (await _context.Users.AnyAsync(user => user.Afm == afm)) );
        }

        public async Task AddUser(UserEntity user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}