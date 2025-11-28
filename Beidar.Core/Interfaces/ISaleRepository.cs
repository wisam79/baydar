using Beidar.Core.Models;

namespace Beidar.Core.Interfaces;

public interface ISaleRepository : IGenericRepository<Sale>
{
    Task<IEnumerable<Sale>> GetSalesByDateRangeAsync(DateTime start, DateTime end);
    Task<decimal> GetTotalSalesAsync(DateTime start, DateTime end);
    Task<int> GetSalesCountAsync(DateTime start, DateTime end);
}
