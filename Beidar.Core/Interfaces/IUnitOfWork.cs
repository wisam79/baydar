using Beidar.Core.Models;

namespace Beidar.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IProductRepository Products { get; }
    ISaleRepository Sales { get; }
    IGenericRepository<SaleItem> SaleItems { get; }
    IGenericRepository<Customer> Customers { get; }
    IGenericRepository<Supplier> Suppliers { get; }
    IGenericRepository<Expense> Expenses { get; }
    IGenericRepository<AppSettings> Settings { get; }
    
    Task<int> CompleteAsync();
}
