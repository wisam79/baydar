using Beidar.Core.Interfaces;
using Beidar.Core.Models;
using Beidar.Data.Context;

namespace Beidar.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public IProductRepository Products { get; private set; }
    public ISaleRepository Sales { get; private set; }
    public IGenericRepository<SaleItem> SaleItems { get; private set; }
    public IGenericRepository<Customer> Customers { get; private set; }
    public IGenericRepository<Supplier> Suppliers { get; private set; }
    public IGenericRepository<Expense> Expenses { get; private set; }
    public IGenericRepository<AppSettings> Settings { get; private set; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Products = new ProductRepository(_context);
        Sales = new SaleRepository(_context);
        SaleItems = new GenericRepository<SaleItem>(_context);
        Customers = new GenericRepository<Customer>(_context);
        Suppliers = new GenericRepository<Supplier>(_context);
        Expenses = new GenericRepository<Expense>(_context);
        Settings = new GenericRepository<AppSettings>(_context);
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}