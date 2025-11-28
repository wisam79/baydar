using Beidar.Core.Interfaces;
using Beidar.Core.Models;
using Beidar.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Beidar.Data.Repositories;

public class SaleRepository : GenericRepository<Sale>, ISaleRepository
{
    public SaleRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Sale>> GetSalesByDateRangeAsync(DateTime start, DateTime end)
    {
        return await _dbSet
            .Where(s => s.SaleDate >= start && s.SaleDate <= end)
            .Include(s => s.Items)
            .ThenInclude(i => i.Product)
            .ToListAsync();
    }

    public async Task<decimal> GetTotalSalesAsync(DateTime start, DateTime end)
    {
        return await _dbSet
            .Where(s => s.SaleDate >= start && s.SaleDate <= end)
            .SumAsync(s => s.Total);
    }
    
    public async Task<int> GetSalesCountAsync(DateTime start, DateTime end)
    {
        return await _dbSet
            .CountAsync(s => s.SaleDate >= start && s.SaleDate <= end);
    }
}
