using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FW.Microservices.Orders.API.Entities;

public sealed class Order
{
	[Key]
	[DatabaseGenerated (DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	public int CustomerId { get; set; }

	[Required]
	public DateTime DateOfOrder { get; set; }

	[Required]
	[Column (TypeName = "REAL")]
	public decimal TotalAmount { get; set; }

	public Customer Customer { get; set; } = default!;

	public OrderDetails OrderDetails { get; set; } = default!;

	public ICollection<OrderItem> OrderItems { get; set; } = [];
}