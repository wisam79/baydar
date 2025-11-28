using Beidar.Core.Models;

namespace Beidar.Core.Interfaces;

public interface ISaleService
{
    Task<Sale> CreateSaleAsync(Sale sale);
    Task<IEnumerable<Sale>> GetRecentSalesAsync();
    Task<Sale?> GetSaleByIdAsync(int id);
}
