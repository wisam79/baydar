using CommunityToolkit.Mvvm.ComponentModel;

namespace Beidar.UI.ViewModels;

public class BaseViewModel : ObservableObject
{
    // Common properties like IsBusy, Title, etc. can go here.
    
    private bool _isBusy;
    public bool IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }

    private string _title = string.Empty;
    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }
}
