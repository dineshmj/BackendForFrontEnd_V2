using FW.Microservices.Orders.API.Entities.GraphQL;

namespace FW.Microservices.Orders.API.DBAccess;

public interface IOrderRepository
{
	Task<IEnumerable<OrderDto>> GetOrderReportAsync ();
}