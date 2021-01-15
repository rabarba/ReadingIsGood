using MediatR;
using MongoDB.Bson;
using ReadingIsGood.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace ReadingIsGood.API.Application.Customers.Queries
{
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, CustomerDto>
    {
        private readonly ICustomerRepository _customerRepository;
        public GetCustomerQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<CustomerDto> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            if (!ObjectId.TryParse(request.CustomerId, out ObjectId customerObjectId))
            {
                //throw new ApiException("Missing format", System.Net.HttpStatusCode.BadRequest);
            }

            var customer = await _customerRepository.GetCustomerAsync(customerObjectId.ToString());
            if (customer == null)
            {
                //throw new ApiException("Customer not found", System.Net.HttpStatusCode.BadRequest);
            }

            return new CustomerDto
            {
                Name = customer.Name,
                Address = customer.Address,
                Email = customer.Email,
                Phone = customer.Phone
            };
        }
    }
}
