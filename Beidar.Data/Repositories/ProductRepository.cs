using Beidar.Core.Interfaces;
using Beidar.Core.Models;
using Beidar.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Beidar.Data.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Product>> GetLowStockProductsAsync(int threshold)
    {
        return await _dbSet.Where(p => p.Stock <= threshold).ToListAsync();
    }
}
