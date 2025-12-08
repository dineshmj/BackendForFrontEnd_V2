using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FW.Microservices.Orders.API.Entities;

public sealed class OrderDetails
{
	[Key]
	[DatabaseGenerated (DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	public int OrderId { get; set; }

	[Required]
	[StringLength (50)]
	public string InvoiceNumber { get; set; } = string.Empty;

	[Required]
	public string PaymentMethod { get; set; } = string.Empty;

	[Required]
	public string DispatchStatus { get; set; } = string.Empty;

	public Order Order { get; set; } = default!;
}