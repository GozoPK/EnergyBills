using System.Text.Json;
using AppApi.DTOs;
using AppApi.Entities;
using AppApi.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AppApi.Data
{
    public class Seed
    {
        public static async Task SeedUserBills(DataContext context, UserManager<UserEntity> userManager, RoleManager<UserRole> roleManager, IMapper mapper)
        {
            if (await context.Users.AnyAsync()) return;

            var roles = new List<UserRole>()
            {
                new UserRole{ Name = "Member" },
                new UserRole{ Name = "Admin" }
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            var userDto = new UserForRegisterDto
            {
                Username = "petros",
                Password = "Password!1",
                Email = "petros@email.com",
                PhoneNumber = "6971234567",
                FirstName = "Πέτρος",
                LastName = "Πέτρου",
                Afm = "218692200",
                AddressStreet = "Κηφισίας",
                AddressNumber = "1",
                PostalCode = "11523",
                City = "Αθήνα",
                AnnualIncome = (decimal)18000.00,
                Iban = "GR1234567891234567891234567"
            };

            var userEntity = mapper.Map<UserEntity>(userDto);

            var billsData = await File.ReadAllTextAsync("Data/BillsSeed.json");
            var bills = JsonSerializer.Deserialize<List<UserBill>>(billsData);

            if (bills == null) return;

            foreach (var bill in bills)
            {
                bill.Id = Guid.NewGuid();
                bill.BillNumber = ((new Random()).Next()).ToString();
                bill.AmmountToReturn = bill.Ammount * (decimal)0.08;
                bill.Status = Status.Approved;
                bill.State = State.Submitted;

                if (bill.Month == Month.December)
                {
                    bill.Status = Status.Pending;
                    bill.AmmountToReturn = 0;
                }
                
                if (bill.Year == 2023)
                {
                    bill.State = State.Saved;
                    bill.Status = Status.Pending;
                    bill.AmmountToReturn = 0;
                }

                userEntity.UserBills.Add(bill);
            }

            var memberRole = await roleManager.FindByNameAsync("Member");
            userEntity.RoleId = memberRole.Id;

            await userManager.CreateAsync(userEntity, userDto.Password);
            await userManager.AddToRoleAsync(userEntity, "Member");

            var admin = new UserEntity
            {
                UserName = "admin",
                Email = "admin@email.com"
            };

            var adminRole = await roleManager.FindByNameAsync("Admin");
            admin.RoleId = adminRole.Id;

            await userManager.CreateAsync(admin, userDto.Password);
            await userManager.AddToRoleAsync(admin, "Admin");
        }
    }
}