using Beidar.UI.Helpers;
using Beidar.UI.ViewModels;
using ModernWpf.Controls;
using System.Windows;
using System.Windows.Controls;

namespace Beidar.UI;

public partial class MainWindow : Window
{
    public MainViewModel ViewModel => (MainViewModel)DataContext;

    public MainWindow(MainViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.IsSettingsSelected)
        {
            ViewModel.Navigate("Settings");
        }
        else if (args.SelectedItem is NavigationViewItem item)
        {
            ViewModel.NavigateToItem(item);
        }
    }

    private void Language_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is ComboBox comboBox && comboBox.SelectedItem is ComboBoxItem item)
        {
            if (item.Tag is string langCode)
            {
                LocalizationHelper.SetLanguage(langCode);
            }
        }
    }
}
