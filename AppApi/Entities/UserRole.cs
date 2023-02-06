using Microsoft.AspNetCore.Identity;

namespace AppApi.Entities
{
    public class UserRole: IdentityRole
    {
        public ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();
    }
}