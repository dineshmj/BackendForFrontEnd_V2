namespace FW.Microservices.Products.API.Entities.Dtos;

public class ProductDetailDto
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public string CategoryName { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int StockQuantity { get; set; }

    public DateTime LastUpdated { get; set; }
}