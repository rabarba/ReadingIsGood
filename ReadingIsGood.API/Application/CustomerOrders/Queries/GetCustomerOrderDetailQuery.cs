using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ReadingIsGood.API.Application.CustomerOrders.Queries
{
    public class GetCustomerOrderDetailQuery : IRequest<OrderDetailDto>
    {
        [Required]
        [FromRoute(Name = "customerId")]
        public string CustomerId { get; set; }

        [Required]
        [FromRoute(Name = "orderId")]
        public string OrderId { get; set; }
    }
}
