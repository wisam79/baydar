using Beidar.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Beidar.UI.ViewModels;

public partial class ProductFormViewModel : ObservableValidator
{
    public Product Product { get; private set; }

    public ProductFormViewModel(Product? product = null)
    {
        Product = product ?? new Product();
        
        // Initialize properties from model
        _name = Product.Name;
        _category = Product.Category;
        _price = Product.Price;
        _cost = Product.Cost;
        _stock = Product.Stock;
        _barcode = Product.Barcode;
        
        ValidateAllProperties();
    }

    [ObservableProperty]
    [Required(ErrorMessage = "Name is required")]
    private string _name = string.Empty;

    [ObservableProperty]
    private string _category = string.Empty;

    [ObservableProperty]
    [Range(0, double.MaxValue, ErrorMessage = "Price must be positive")]
    private decimal _price;

    [ObservableProperty]
    [Range(0, double.MaxValue, ErrorMessage = "Cost must be positive")]
    private decimal _cost;

    [ObservableProperty]
    private int _stock;

    [ObservableProperty]
    private string _barcode = string.Empty;

    public Product GetUpdatedProduct()
    {
        Product.Name = Name;
        Product.Category = Category;
        Product.Price = Price;
        Product.Cost = Cost;
        Product.Stock = Stock;
        Product.Barcode = Barcode;
        return Product;
    }
}
