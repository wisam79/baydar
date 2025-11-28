using Beidar.Core.Interfaces;
using Beidar.Core.DTOs;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Beidar.UI.ViewModels;

public partial class DashboardViewModel : BaseViewModel
{
    private readonly IDashboardService _dashboardService;

    private DashboardStatsDto _stats = new();
    public DashboardStatsDto Stats
    {
        get => _stats;
        set => SetProperty(ref _stats, value);
    }

    public DashboardViewModel(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
        Title = "Dashboard";
        Stats = new DashboardStatsDto();
        LoadDataCommand.Execute(null);
    }

    [RelayCommand]
    private async Task LoadData()
    {
        IsBusy = true;
        try
        {
            Stats = await _dashboardService.GetDashboardStatsAsync();
        }
        finally
        {
            IsBusy = false;
        }
    }
}
