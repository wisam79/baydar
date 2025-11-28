using Beidar.Core.Interfaces;
using Beidar.Core.Models;

namespace Beidar.Core.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _unitOfWork.Products.GetAllAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _unitOfWork.Products.GetByIdAsync(id);
    }

    public async Task<Product> AddProductAsync(Product product)
    {
        // Business Logic: Validate Product
        if (string.IsNullOrWhiteSpace(product.Name))
            throw new ArgumentException("Product name is required.");
            
        if (product.Price < 0)
            throw new ArgumentException("Price cannot be negative.");

        await _unitOfWork.Products.AddAsync(product);
        await _unitOfWork.CompleteAsync();
        return product;
    }

    public async Task UpdateProductAsync(Product product)
    {
         // Business Logic: Validate Product
        if (string.IsNullOrWhiteSpace(product.Name))
            throw new ArgumentException("Product name is required.");

        _unitOfWork.Products.Update(product);
        await _unitOfWork.CompleteAsync();
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id);
        if (product != null)
        {
            _unitOfWork.Products.Remove(product);
            await _unitOfWork.CompleteAsync();
        }
    }
}
