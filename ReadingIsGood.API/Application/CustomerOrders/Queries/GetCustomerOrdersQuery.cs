using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ReadingIsGood.API.Application.CustomerOrders.Queries
{
    public class GetCustomerOrdersQuery : IRequest<OrderDto>
    {
        [Required]
        [FromRoute(Name = "customerId")]
        public string CustomerId { get; set; }
    }
}
