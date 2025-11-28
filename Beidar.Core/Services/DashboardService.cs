using Beidar.Core.DTOs;
using Beidar.Core.Interfaces;

namespace Beidar.Core.Services;

public class DashboardService : IDashboardService
{
    private readonly IUnitOfWork _unitOfWork;

    public DashboardService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<DashboardStatsDto> GetDashboardStatsAsync()
    {
        var today = DateTime.Today;
        var tomorrow = today.AddDays(1);

        // Use specific repository methods for efficient querying
        var totalSales = await _unitOfWork.Sales.GetTotalSalesAsync(today, tomorrow);
        var totalOrders = await _unitOfWork.Sales.GetSalesCountAsync(today, tomorrow);
        
        // For low stock, we might need to configure threshold from settings
        // For now, hardcoded or fetched from settings repository
        var settings = (await _unitOfWork.Settings.GetAllAsync()).FirstOrDefault();
        int lowStockThreshold = settings?.LowStockThreshold ?? 10;

        var lowStockProducts = await _unitOfWork.Products.GetLowStockProductsAsync(lowStockThreshold);
        int lowStockCount = lowStockProducts.Count();

        // Profit calculation (Simplified: Total Sales - Total Cost of items sold)
        // This is tricky without specific query. For now, 20% of sales as placeholder if no cost data
        // Or fetch sales with items to calculate.
        // Let's assume 0 for now or implement a GetProfitAsync in SaleRepository later.
        decimal profit = 0; 
        // Real implementation would require:
        // var salesToday = await _unitOfWork.Sales.GetSalesByDateRangeAsync(today, tomorrow);
        // profit = salesToday.Sum(s => s.Total - s.Items.Sum(i => i.UnitCost * i.Quantity));

        return new DashboardStatsDto
        {
            TotalSalesToday = totalSales,
            TotalOrdersToday = totalOrders,
            NetProfit = profit,
            LowStockCount = lowStockCount,
            Notifications = new List<string>() // Populate if needed
        };
    }
}
