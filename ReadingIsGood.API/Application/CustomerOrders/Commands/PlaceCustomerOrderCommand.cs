using MediatR;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReadingIsGood.API.Application.CustomerOrders.Commands
{
    public class PlaceCustomerOrderCommand : IRequest<string>
    {
        internal string CustomerId { get; set; }

        [Required]
        public List<ProductDto> Products { get; set; }
    }
}
