namespace FW.Microservices.Orders.API.Entities.GraphQL;

public sealed class OrderDto
{
	public int OrderId { get; set; }

	public string DateOfOrder { get; set; } = string.Empty;

	public decimal TotalAmount { get; set; }

	public string PaymentMethod { get; set; } = string.Empty;

	public string InvoiceNumber { get; set; } = string.Empty;

	public int NumberOfItems { get; set; }

	public string CustomerName { get; set; } = string.Empty;

	public string DispatchStatus { get; set; } = string.Empty;
}