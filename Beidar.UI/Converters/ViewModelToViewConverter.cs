using System.Globalization;
using System.Windows.Data;

namespace Beidar.UI.Converters;

public class ViewModelToViewConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null) return null;

        // Simple mapping based on Type name conventions or Dependency Injection
        // Since we set CurrentView to ViewModel, we need to find the corresponding View.
        // Ideally, we use DataTemplates in App.xaml, but let's see if we can do it here or just use DataTemplates.
        
        // Best practice in MVVM is to use DataTemplates in Resources. 
        // Let's return the value as is and handle mapping in XAML.
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
