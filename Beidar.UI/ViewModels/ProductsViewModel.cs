using Beidar.Core.Interfaces;
using Beidar.Core.Models;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Beidar.UI.ViewModels;

public partial class ProductsViewModel : BaseViewModel
{
    private readonly IProductService _productService;

    public ObservableCollection<Product> Products { get; } = new();

    private Product? _selectedProduct;
    public Product? SelectedProduct
    {
        get => _selectedProduct;
        set => SetProperty(ref _selectedProduct, value);
    }

    public ProductsViewModel(IProductService productService)
    {
        _productService = productService;
        Title = "Products";
        LoadProductsCommand.Execute(null);
    }

    [RelayCommand]
    private async Task LoadProducts()
    {
        IsBusy = true;
        try
        {
            Products.Clear();
            var products = await _productService.GetAllProductsAsync();
            foreach (var product in products)
            {
                Products.Add(product);
            }
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task AddProduct()
    {
        var vm = new ProductFormViewModel();
        var dialog = new Views.Products.ProductFormDialog(vm);
        
        var result = await dialog.ShowAsync();
        if (result == ModernWpf.Controls.ContentDialogResult.Primary)
        {
            if (vm.HasErrors)
            {
                // Ideally prevent closing, but for now simplistic handling
                // ModernWpf ContentDialog validation requires more wiring.
            }

            var newProduct = vm.GetUpdatedProduct();
            await _productService.AddProductAsync(newProduct);
            Products.Add(newProduct);
        }
    }

    [RelayCommand]
    private async Task EditProduct(Product product)
    {
        if (product == null) return;

        var vm = new ProductFormViewModel(product);
        var dialog = new Views.Products.ProductFormDialog(vm);

        var result = await dialog.ShowAsync();
        if (result == ModernWpf.Controls.ContentDialogResult.Primary)
        {
            var updatedProduct = vm.GetUpdatedProduct();
            await _productService.UpdateProductAsync(updatedProduct);
            
            // Refresh list item (simple way: reload or replace)
            var index = Products.IndexOf(product);
            if (index != -1)
            {
                Products[index] = updatedProduct;
            }
        }
    }

    [RelayCommand]
    private async Task DeleteProduct()
    {
        if (SelectedProduct == null) return;

        await _productService.DeleteProductAsync(SelectedProduct.Id);
        Products.Remove(SelectedProduct);
    }
}
