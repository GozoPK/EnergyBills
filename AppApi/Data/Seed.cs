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
        public static async Task SeedUserBills(DataContext context, UserManager<UserEntity> userManager, IMapper mapper)
        {
            if (await context.Users.AnyAsync()) return;

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

                userEntity.UserBills.Add(bill);
            }

            var result = await userManager.CreateAsync(userEntity, userDto.Password);
        }
    }
}