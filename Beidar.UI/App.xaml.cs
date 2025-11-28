using Beidar.Core.Interfaces;
using Beidar.Core.Services;
using Beidar.Data.Context;
using Beidar.Data.Repositories;
using Beidar.UI.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ModernWpf;
using System;
using System.IO;
using System.Windows;
using Serilog;

namespace Beidar.UI;

public partial class App : Application
{
    public new static App Current => (App)Application.Current;
    public IServiceProvider Services { get; }

    public App()
    {
        // Configure Serilog
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
            .MinimumLevel.Debug()
            .CreateLogger();

        // Global Exception Handling
        AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            Log.Fatal((Exception)e.ExceptionObject, "AppDomain Unhandled Exception");

        DispatcherUnhandledException += (s, e) =>
        {
            Log.Fatal(e.Exception, "Dispatcher Unhandled Exception");
            e.Handled = true;
            MessageBox.Show($"An error occurred: {e.Exception.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        };

        Services = ConfigureServices();
    }

    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        // Database
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite("Data Source=beidar.db");
        });

        // Repositories
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Services
        services.AddScoped<IDashboardService, DashboardService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ISaleService, SaleService>();

        // ViewModels
        services.AddTransient<MainViewModel>();
        services.AddTransient<DashboardViewModel>();
        services.AddTransient<ProductsViewModel>();
        services.AddTransient<SalesViewModel>();

        // Views
        services.AddTransient<MainWindow>();

        return services.BuildServiceProvider();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        try
        {
            Log.Information("Application Starting...");

            // Apply Theme
            ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
            ThemeManager.Current.AccentColor = System.Windows.Media.Color.FromRgb(0, 200, 150); // #00C896

            var mainWindow = Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
            
            Log.Information("MainWindow Shown.");
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Error in OnStartup");
            MessageBox.Show($"Startup Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            Shutdown(-1);
        }
    }

    protected override void OnExit(ExitEventArgs e)
    {
        Log.CloseAndFlush();
        base.OnExit(e);
    }
}
