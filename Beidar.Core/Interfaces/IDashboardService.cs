using Beidar.Core.DTOs;

namespace Beidar.Core.Interfaces;

public interface IDashboardService
{
    Task<DashboardStatsDto> GetDashboardStatsAsync();
}
