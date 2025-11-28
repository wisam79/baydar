using Beidar.Core.Interfaces;
using Beidar.Core.Models;

namespace Beidar.Core.Services;

public class SaleService : ISaleService
{
    private readonly IUnitOfWork _unitOfWork;

    public SaleService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Sale> CreateSaleAsync(Sale sale)
    {
        if (sale.Items == null || !sale.Items.Any())
            throw new InvalidOperationException("Cannot create a sale with no items.");

        // Business Logic: Check Stock and Update
        foreach (var item in sale.Items)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(item.ProductId);
            if (product == null)
                throw new InvalidOperationException($"Product with ID {item.ProductId} not found.");

            if (product.Stock < item.Quantity)
            {
                // You might want to check Settings.AllowNegativeStock here
                throw new InvalidOperationException($"Insufficient stock for product '{product.Name}'. Available: {product.Stock}");
            }

            product.Stock -= item.Quantity;
            _unitOfWork.Products.Update(product);
        }

        // Generate Invoice Number (Simple logic for now)
        sale.InvoiceNumber = $"INV-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 4).ToUpper()}";
        sale.SaleDate = DateTime.Now;

        await _unitOfWork.Sales.AddAsync(sale);
        await _unitOfWork.CompleteAsync();

        return sale;
    }

    public async Task<IEnumerable<Sale>> GetRecentSalesAsync()
    {
        // Get sales for the last 30 days for example
        var end = DateTime.Now;
        var start = end.AddDays(-30);
        return await _unitOfWork.Sales.GetSalesByDateRangeAsync(start, end);
    }

    public async Task<Sale?> GetSaleByIdAsync(int id)
    {
        return await _unitOfWork.Sales.GetByIdAsync(id);
    }
}
