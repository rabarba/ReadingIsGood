using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ReadingIsGood.API.Application.CustomerOrders.Queries
{
    public class GetCustomerOrderDetailQuery : IRequest<OrderDetailDto>
    {
        [FromRoute(Name = "customerId")]
        public string CustomerId { get; set; }

        [FromRoute(Name = "orderId")]
        public string OrderId { get; set; }
    }
}
