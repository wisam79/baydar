using System.Windows;

namespace Beidar.UI.Helpers;

public static class LocalizationHelper
{
    public static void SetLanguage(string cultureCode)
    {
        var dict = new ResourceDictionary();
        string source = string.Empty;
        FlowDirection flowDirection = FlowDirection.LeftToRight;

        switch (cultureCode.ToLower())
        {
            case "ar":
                source = "Assets/Strings/Strings.ar.xaml";
                flowDirection = FlowDirection.RightToLeft;
                break;
            case "en":
            default:
                source = "Assets/Strings/Strings.en.xaml";
                flowDirection = FlowDirection.LeftToRight;
                break;
        }

        dict.Source = new Uri(source, UriKind.Relative);

        // Remove old dictionary if exists (we assume it's the last one or we tag it)
        // For simplicity, we'll just find any dict that looks like a String dict and remove it
        // Or simpler: The Strings dict should be loaded in App.xaml. We can replace it.
        
        // A robust way is to keep a reference or tag it.
        // Let's assume we add it to MergedDictionaries.
        
        var appResources = Application.Current.Resources;
        ResourceDictionary? oldDict = null;

        foreach (var d in appResources.MergedDictionaries)
        {
            if (d.Source != null && d.Source.OriginalString.Contains("Strings."))
            {
                oldDict = d;
                break;
            }
        }

        if (oldDict != null)
        {
            appResources.MergedDictionaries.Remove(oldDict);
        }

        appResources.MergedDictionaries.Add(dict);

        // Set FlowDirection for the main window
        if (Application.Current.MainWindow != null)
        {
            Application.Current.MainWindow.FlowDirection = flowDirection;
        }
    }
}
