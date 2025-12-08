using Microsoft.EntityFrameworkCore;

using FW.Microservices.Products.API.Entities;
using FW.Microservices.Products.API.Entities.Dtos;

namespace FW.Microservices.Products.API.DBAccess;

public sealed class ProductRepository
    : IProductRepository
{
    private readonly ProductDbContext _context;

    public ProductRepository(ProductDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Inventory)
            .ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Inventory)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddProductAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateProductAsync(Product product)
    {
        _context.Products.Update(product);

        if (product.Inventory != null)
        {
            _context.Inventory.Update(product.Inventory);
        }

        await _context.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(Product product)
    {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ProductDetailDto>> GetInventoryReportAsync()
    {
        var report = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Inventory)
            .Select(p => new ProductDetailDto
            {
                ProductId = p.Id,
                ProductName = p.Name,
                CategoryName = p.Category.Name,
                Price = p.Price,
                StockQuantity = p.Inventory!.StockQuantity,
                LastUpdated = p.Inventory!.LastUpdated
            })
            .OrderBy(dto => dto.CategoryName)
            .ThenBy(dto => dto.ProductName)
            .ToListAsync();

        return report;
    }

    public async Task UpdateStockLevelAsync(int productId, int newStockLevel)
    {
        var inventory = await _context.Inventory
            .FirstOrDefaultAsync(i => i.ProductId == productId);

        if (inventory == null)
        {
            throw new ArgumentException($"Inventory record not found for Product ID {productId}");
        }

        inventory.StockQuantity = newStockLevel;
        inventory.LastUpdated = DateTime.UtcNow; // Set updated time

        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
    {
        return await _context.Categories.ToListAsync();
    }
}