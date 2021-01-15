using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ReadingIsGood.API.Application.Customers.Queries
{
    public class GetCustomerQuery : IRequest<CustomerDto>
    {
        [FromRoute]
        public string CustomerId { get; set; }
    }
}
