using AppApi.Data;
using AppApi.DTOs;
using AppApi.Entities;
using AppApi.Helpers;
using AppApi.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AppApi.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IMapper _mapper;
        public UserRepository(UserManager<UserEntity> userManager, DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _userManager = userManager;
            _context = context;
        }

        public async Task<UserEntity> GetUserAsync(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<UserEntity> GetUserWithBillsAsync(string username)
        {
            return await _context.Users
                    .Include(user => user.UserBills)
                    .FirstOrDefaultAsync(user => user.UserName == username);
        }

        public async Task<PagedList<UserBillToReturnDto>> GetUserBillsAsync(UserParams userParams)
        {
            var user = await GetUserAsync(userParams.Username);

            var query = _context.Bills.Where(bill => bill.UserEntityId == user.Id);

            query = userParams.Type switch
            {
                "electricity" => query.Where(bill => bill.Type == BillType.Electricity),
                "naturalgas" => query.Where(bill => bill.Type == BillType.NaturalGas),
                "both" => query.Where(bill => bill.Type == BillType.Both),
                _ => query
            };

            query = userParams.Status switch
            {
                "approved" => query.Where(bill => bill.Status == Status.Approved),
                "rejected" => query.Where(bill => bill.Status == Status.Rejected),
                "pending" => query.Where(bill => bill.Status == Status.Pending),
                _ => query
            };

            var minDateExpression = 12*(userParams.MinYear - 2022) + userParams.MinMonth;
            query = query.Where(bill => (12*(bill.Year - 2022) + (int)bill.Month) >= minDateExpression);  

            var maxDateExpression = 12*(userParams.MaxYear - 2022) + userParams.MaxMonth;
            query = query.Where(bill => (12*(bill.Year - 2022) + (int)bill.Month) <= maxDateExpression);            

            query = userParams.OrderBy switch
            {
                "dateoldest" => query.OrderBy(bill => bill.Year).ThenBy(bill => bill.Month).ThenBy(bill => bill.Type),
                _ => query.OrderByDescending(bill => bill.Year).ThenByDescending(bill => bill.Month).ThenBy(bill => bill.Type)
            };

            var totalCount = await query.CountAsync();
            var userBills = await query.Skip((userParams.PageNumber -1) * userParams.PageSize).Take(userParams.PageSize).ToListAsync();
            var list = _mapper.Map<IEnumerable<UserBillToReturnDto>>(userBills);

            return new PagedList<UserBillToReturnDto>(list, userParams.PageNumber, userParams.PageSize, totalCount);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

    }
}