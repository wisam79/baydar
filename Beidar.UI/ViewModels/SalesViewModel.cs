using Beidar.Core.Enums;
using Beidar.Core.Interfaces;
using Beidar.Core.Models;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Beidar.UI.ViewModels;

public partial class SalesViewModel : BaseViewModel
{
    private readonly IProductService _productService;
    private readonly ISaleService _saleService;

    // Product Selection
    public ObservableCollection<Product> Products { get; } = new();
    
    private Product? _selectedProduct;
    public Product? SelectedProduct
    {
        get => _selectedProduct;
        set => SetProperty(ref _selectedProduct, value);
    }
    
    private string _searchQuery = string.Empty;
    public string SearchQuery
    {
        get => _searchQuery;
        set
        {
            if (SetProperty(ref _searchQuery, value))
            {
                FilterProducts();
            }
        }
    }

    // Cart
    public ObservableCollection<SaleItem> CartItems { get; } = new();

    private SaleItem? _selectedCartItem;
    public SaleItem? SelectedCartItem
    {
        get => _selectedCartItem;
        set => SetProperty(ref _selectedCartItem, value);
    }

    // Totals
    private decimal _subtotal;
    public decimal Subtotal
    {
        get => _subtotal;
        set => SetProperty(ref _subtotal, value);
    }

    private decimal _discount;
    public decimal Discount
    {
        get => _discount;
        set 
        {
            if (SetProperty(ref _discount, value))
                CalculateTotals();
        }
    }

    private decimal _vat;
    public decimal VAT
    {
        get => _vat;
        set => SetProperty(ref _vat, value);
    }

    private decimal _total;
    public decimal Total
    {
        get => _total;
        set => SetProperty(ref _total, value);
    }

    private decimal _amountPaid;
    public decimal AmountPaid
    {
        get => _amountPaid;
        set 
        {
            if (SetProperty(ref _amountPaid, value))
                CalculateChange();
        }
    }

    private decimal _change;
    public decimal Change
    {
        get => _change;
        set => SetProperty(ref _change, value);
    }

    private List<Product> _allProducts = new();

    public SalesViewModel(IProductService productService, ISaleService saleService)
    {
        _productService = productService;
        _saleService = saleService;
        Title = "New Sale";
        LoadProductsCommand.Execute(null);
    }

    [RelayCommand]
    private async Task LoadProducts()
    {
        IsBusy = true;
        try
        {
            var products = await _productService.GetAllProductsAsync();
            _allProducts = products.ToList();
            FilterProducts();
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void FilterProducts()
    {
        Products.Clear();
        var query = SearchQuery?.ToLower() ?? "";
        var filtered = _allProducts.Where(p => 
            p.Name.ToLower().Contains(query) || 
            p.Barcode.Contains(query) ||
            p.Category.ToLower().Contains(query));

        foreach (var p in filtered)
        {
            Products.Add(p);
        }
    }

    [RelayCommand]
    private void AddToCart(Product? product)
    {
        var p = product ?? SelectedProduct;
        if (p == null) return;

        var existingItem = CartItems.FirstOrDefault(i => i.ProductId == p.Id);
        if (existingItem != null)
        {
            existingItem.Quantity++;
            existingItem.LineTotal = existingItem.Quantity * existingItem.UnitPrice;
            // Trigger property change notification for UI update if needed, 
            // or just replace/reload. For ObservableCollection update, 
            // replacing the item or implementing INotifyPropertyChanged on SaleItem is best.
            // Here we just force a total recalc.
            // A better way for real UI is to having SaleItemViewModel.
            // We will cheat slightly by removing and re-adding index or using a wrapper.
            // For simplicity in this step, let's just recalc totals.
        }
        else
        {
            CartItems.Add(new SaleItem
            {
                ProductId = p.Id,
                ProductName = p.Name,
                UnitPrice = p.Price,
                UnitCost = p.Cost,
                Quantity = 1,
                LineTotal = p.Price,
                Product = p // Reference for EF
            });
        }
        CalculateTotals();
    }

    [RelayCommand]
    private void RemoveFromCart()
    {
        if (SelectedCartItem == null) return;
        CartItems.Remove(SelectedCartItem);
        CalculateTotals();
    }

    private void CalculateTotals()
    {
        Subtotal = CartItems.Sum(i => i.Quantity * i.UnitPrice);
        // VAT Logic (e.g. 0% or from Settings) - Hardcoded 0% for now
        decimal vatRate = 0; 
        VAT = Subtotal * vatRate;
        Total = Subtotal + VAT - Discount;
        CalculateChange();
    }

    private void CalculateChange()
    {
        Change = AmountPaid - Total;
    }

    [RelayCommand]
    private async Task Checkout()
    {
        if (!CartItems.Any()) return;

        IsBusy = true;
        try
        {
            var sale = new Sale
            {
                Items = CartItems.ToList(),
                Subtotal = Subtotal,
                Discount = Discount,
                VAT = VAT,
                Total = Total,
                PaymentMethod = PaymentMethod.Cash, // Default
                Status = SaleStatus.Completed
            };

            await _saleService.CreateSaleAsync(sale);

            // Success
            MessageBox.Show($"Sale Completed! Invoice: {sale.InvoiceNumber}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            
            // Reset
            CartItems.Clear();
            Discount = 0;
            AmountPaid = 0;
            CalculateTotals();
            
            // Reload products to update stock in UI
            await LoadProducts();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Sale Failed", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }
}
