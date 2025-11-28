using Beidar.Core.Models;

namespace Beidar.Core.Interfaces;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<IEnumerable<Product>> GetLowStockProductsAsync(int threshold);
}
