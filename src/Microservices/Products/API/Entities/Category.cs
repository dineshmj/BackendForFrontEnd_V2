using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FW.Microservices.Products.API.Entities;

public sealed class Category
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [JsonIgnore]
    public ICollection<Product> Products { get; set; } = new List<Product>();
}