using Microsoft.EntityFrameworkCore;

using FW.Microservices.Orders.API.Entities.GraphQL;

namespace FW.Microservices.Orders.API.DBAccess;

public sealed class OrderRepository
	: IOrderRepository
{
	private readonly OrderDbContext _context;

	public OrderRepository (OrderDbContext context)
	{
		_context = context;
	}

	// Implements the final SELECT query using LINQ and EF Core
	public async Task<IEnumerable<OrderDto>> GetOrderReportAsync ()
	{
		var query = _context.Orders
			.Include (o => o.Customer)
			.Include (o => o.OrderDetails)
			.Include (o => o.OrderItems)
			.Select (o => new OrderDto
			{
				OrderId = o.Id,
				DateOfOrder = $"{o.DateOfOrder.ToUniversalTime ().ToString ("s")}Z",
				TotalAmount = o.TotalAmount,
				PaymentMethod = o.OrderDetails.PaymentMethod,
				InvoiceNumber = o.OrderDetails.InvoiceNumber,

				// Equivalent to SUM(OI.Quantity) using LINQ Sum()
				NumberOfItems = o.OrderItems.Sum (oi => oi.Quantity),

				// Equivalent to C.FirstName || ' ' || C.LastName
				CustomerName = $"{o.Customer.FirstName} {o.Customer.LastName}",

				DispatchStatus = o.OrderDetails.DispatchStatus
			});

		return await query.ToListAsync ();
	}
}