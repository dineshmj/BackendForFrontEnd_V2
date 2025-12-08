using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FW.Microservices.Products.BFFWeb.Entities;

public sealed class Inventory
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(Product))]
    public int ProductId { get; set; }

    [Required]
    public int StockQuantity { get; set; }

    public DateTime LastUpdated { get; set; }

    public Product Product { get; set; } = null!;
}