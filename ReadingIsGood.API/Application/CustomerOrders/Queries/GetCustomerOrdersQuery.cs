using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ReadingIsGood.API.Application.CustomerOrders.Queries
{
    public class GetCustomerOrdersQuery : IRequest<OrderDto>
    {
        [FromRoute(Name = "customerId")]
        public string CustomerId { get; set; }
    }
}
