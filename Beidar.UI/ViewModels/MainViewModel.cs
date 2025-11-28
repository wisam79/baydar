using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using ModernWpf.Controls;
using System.Windows.Controls;

namespace Beidar.UI.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    private readonly IServiceProvider _serviceProvider;

    private object _currentView = new object();
    public object CurrentView
    {
        get => _currentView;
        set => SetProperty(ref _currentView, value);
    }

    public MainViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        // Load Dashboard by default
        Navigate("Dashboard");
    }

    [RelayCommand]
    public void Navigate(string destination)
    {
        switch (destination)
        {
            case "Dashboard":
                CurrentView = _serviceProvider.GetRequiredService<DashboardViewModel>();
                Title = "Dashboard";
                break;
            case "Sales":
                CurrentView = _serviceProvider.GetRequiredService<SalesViewModel>();
                Title = "Sales";
                break;
            case "Products":
                CurrentView = _serviceProvider.GetRequiredService<ProductsViewModel>();
                Title = "Products";
                break;
            // Add other cases
            default:
                break;
        }
    }
    
    // This method is used by the NavigationView in MainWindow
    public void NavigateToItem(NavigationViewItem item)
    {
        if (item?.Content is string destination)
        {
            Navigate(destination);
        }
    }
}
