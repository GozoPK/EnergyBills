using AppApi.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AppApi.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<UserEntity> FindByNameWithRoleAsync(this UserManager<UserEntity> userManager, string username)
        {
            return await userManager.Users.Include(user => user.Role).FirstOrDefaultAsync(user => user.UserName == username);
        }

        public static async Task<IEnumerable<UserEntity>> GetAdminUsersAsync(this UserManager<UserEntity> userManager)
        {
            return await userManager.Users
                .Include(user => user.Role)
                .Where(user => user.Role.Name == "Admin")
                .OrderBy(user => user.UserName)
                .ToListAsync();
        }
    }
}