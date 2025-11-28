using Beidar.UI.ViewModels;
using ModernWpf.Controls;

namespace Beidar.UI.Views.Products;

public partial class ProductFormDialog : ContentDialog
{
    public ProductFormDialog(ProductFormViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
