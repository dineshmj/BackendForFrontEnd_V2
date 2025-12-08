using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FW.Microservices.Orders.API.Entities;

public sealed class Customer
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	[Required]
	[StringLength(50)]
	public string FirstName { get; set; } = string.Empty;

	[Required]
	[StringLength(50)]
	public string LastName { get; set; } = string.Empty;

	public ICollection<Order> Orders { get; set; } = [];
}