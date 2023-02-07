using AppApi.Data;
using AppApi.DTOs;
using AppApi.Entities;
using AppApi.Helpers;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AppApi.Services
{
    public class BillsRepository : IBillsRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public BillsRepository(DataContext context, IMapper mapper)
        {          
            _mapper = mapper;
            _context = context;
        }

        public async Task<PagedList<UserBillToReturnDto>> GetBillsAsync(UserParams userParams)
        {
            var query = _context.Bills
                .Where(bill => bill.Status == Status.Pending && bill.State == State.Submitted)
                .OrderByDescending((bill => bill.DateOfCreation));

            var totalCount = await query.CountAsync();
            var bills = await query.Skip((userParams.PageNumber -1) * userParams.PageSize).Take(userParams.PageSize).ToListAsync();

            var billsList = _mapper.Map<IEnumerable<UserBillToReturnDto>>(bills);

            return new PagedList<UserBillToReturnDto>(billsList, userParams.PageNumber, userParams.PageSize, totalCount);
        }

        public async Task<UserBill> GetBillByIdAsync(string id)
        {
            return await _context.Bills.Include(bill => bill.UserEntity).FirstOrDefaultAsync(bill => bill.Id.ToString() == id);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

    }
}