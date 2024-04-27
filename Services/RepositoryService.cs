using Microsoft.EntityFrameworkCore;
using WalletApi.Data;
using WalletApi.Models.Entities;
using WalletApi.Services.Interfaces;
namespace WalletApi.Services
{
    public class RepositoryService(AppDbContext context) : IRepositoryService
    {
        private readonly AppDbContext _context = context;

        public async Task<bool> AddAsync<T>(T entity) where T : class
        {
            _context.Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteAsync<T>(T entity) where T : class
        {
            _context.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public IQueryable<T> GetAllAsync<T>() where T : class
        {
            return _context.Set<T>();
        }

        public async Task<T?> GetByIdAsync<T>(string Id) where T : class
        {
            return await _context.FindAsync<T>(Id);
        }

        public async Task<bool> UpdateAsync<T>(T entity) where T : class
        {
            _context.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }
 

        public async Task<Wallet?> GetWalletByUserIdAndCurrencyAsync(string userId, string currency)
        {
            return await _context.Wallets
                .Include(w => w.AppUser)  
                .FirstOrDefaultAsync(w => w.AppUserId == userId && w.Currency == currency);
        }

    }

}
