using AppApi.Data;
using AppApi.DTOs;
using AppApi.Entities;
using AppApi.Extensions;
using AppApi.Helpers;
using AppApi.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AppApi.Services
{
    public class UserBillsRepository : IUserBillsRepository
    {
        private readonly DataContext _context;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IMapper _mapper;
        public UserBillsRepository(UserManager<UserEntity> userManager, DataContext context, IMapper mapper)
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

            query = query.ApplyUserParameters(userParams);

            var totalCount = await query.CountAsync();
            var userBills = await query.Skip((userParams.PageNumber -1) * userParams.PageSize).Take(userParams.PageSize).ToListAsync();
            var list = _mapper.Map<IEnumerable<UserBillToReturnDto>>(userBills);

            return new PagedList<UserBillToReturnDto>(list, userParams.PageNumber, userParams.PageSize, totalCount);
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

    }
}