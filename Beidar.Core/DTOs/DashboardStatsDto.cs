namespace Beidar.Core.DTOs;

public class DashboardStatsDto
{
    public decimal TotalSalesToday { get; set; }
    public int TotalOrdersToday { get; set; }
    public decimal NetProfit { get; set; }
    public int LowStockCount { get; set; }
    public List<string> Notifications { get; set; } = new();
}
