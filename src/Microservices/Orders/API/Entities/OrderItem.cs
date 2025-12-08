using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FW.Microservices.Orders.API.Entities;

public sealed class OrderItem
{
	[Key]
	[DatabaseGenerated (DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	public int OrderId { get; set; }

	[Required]
	public string ProductId { get; set; } = string.Empty;

	[Required]
	public int Quantity { get; set; }

	public Order Order { get; set; } = default!;
}