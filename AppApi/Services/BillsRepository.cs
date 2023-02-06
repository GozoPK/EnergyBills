using AppApi.Data;
using AppApi.DTOs;
using AppApi.Entities;
using AppApi.Helpers;
using Microsoft.EntityFrameworkCore;

namespace AppApi.Services
{
    public class BillsRepository : IBillsRepository
    {
        private readonly DataContext _context;
        public BillsRepository(DataContext context)
        {          
            _context = context;
        }

        public async Task<IEnumerable<UserBill>> GetBillsAsync()
        {
            return await _context.Bills
                .Where(bill => bill.Status  == Status.Pending && bill.State == State.Submitted)
                .OrderBy(bill => bill.DateOfCreation)
                .ToListAsync();
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