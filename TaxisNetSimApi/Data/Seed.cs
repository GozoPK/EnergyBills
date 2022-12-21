using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using TaxisNetSimApi.Entities;

namespace TaxisNetSimApi.Data
{
    public class Seed
    {
        public static async Task SeedUsers(UserContext context)
        {
            if (await context.Users.AnyAsync()) return;

            var userData = await System.IO.File.ReadAllTextAsync("Data/TaxisnetSeed.json");
            var users = JsonSerializer.Deserialize<List<TaxisNetUserEntity>>(userData);

            if (users == null) return;

            foreach (var user in users)
            {
                user.Id = Guid.NewGuid();

                using var hmac = new HMACSHA512();

                user.PasswordSalt = hmac.Key;
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Password!1"));

                await context.Users.AddAsync(user);
            }

            await context.SaveChangesAsync();
        }
    }
}