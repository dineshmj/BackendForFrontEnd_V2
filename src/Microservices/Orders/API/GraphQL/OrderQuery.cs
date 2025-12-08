using HotChocolate.Authorization;

using FW.Microservices.Orders.API.DBAccess;
using FW.Microservices.Orders.API.Entities.GraphQL;

namespace FW.Microservices.Orders.API.GraphQL.Queries
{
	public sealed class OrderQuery
	{
		[Authorize]
		[AllowAnonymous]
		[UsePaging]
		[UseProjection]
		[UseFiltering]
		[UseSorting]
		public async Task<IEnumerable<OrderDto>> GetOrderReport ([Service] IOrderRepository repository)
		{
			return await repository.GetOrderReportAsync ();
		}
	}
}