using MediatR;
using System.Collections.Generic;

namespace ReadingIsGood.API.Application.CustomerOrders.Commands
{
    public class PlaceCustomerOrderCommand : IRequest<string>
    {
        internal string CustomerId { get; set; }
        public List<ProductDto> Products { get; set; }
    }
}
