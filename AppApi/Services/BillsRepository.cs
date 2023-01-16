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
                .Where(bill => bill.Status  == Status.Pending)
                .OrderBy(bill => bill.DateOfCreation)
                .ToListAsync();
        }

        public Task UpdateBillRequest(string username, UserBillToCreateDto userBillToCreate)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

    }
}