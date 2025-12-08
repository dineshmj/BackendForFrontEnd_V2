using FW.Microservices.Products.API.Entities;
using FW.Microservices.Products.API.Entities.Dtos;

namespace FW.Microservices.Products.API.DBAccess;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllProductsAsync();

    Task<Product?> GetProductByIdAsync(int id);

    Task AddProductAsync(Product product);

    Task UpdateProductAsync(Product product);

    Task DeleteProductAsync(Product product);

    Task<IEnumerable<ProductDetailDto>> GetInventoryReportAsync();

    Task UpdateStockLevelAsync(int productId, int newStockLevel);

    Task<IEnumerable<Category>> GetAllCategoriesAsync();
}