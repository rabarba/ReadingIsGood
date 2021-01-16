using MediatR;
using ReadingIsGood.Domain.Documents;
using ReadingIsGood.Domain.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ReadingIsGood.API.Application.CustomerOrders.Commands
{
    public class PlaceCustomerOrderCommandHandler : IRequestHandler<PlaceCustomerOrderCommand, string>
    {
        private readonly ICustomerOrderRepository _customerOrderRepository;

        public PlaceCustomerOrderCommandHandler(ICustomerOrderRepository customerOrderRepository)
        {
            _customerOrderRepository = customerOrderRepository;
        }

        public Task<string> Handle(PlaceCustomerOrderCommand request, CancellationToken cancellationToken)
        {
            var customerOrder = new CustomerOrder
            {
                CustomerId = request.CustomerId,
                Products = request.Products.Select(x=> new Product
                {
                    Id = x.Id,
                    Quantity = x.Quantity
                }).ToList()
            };

            return _customerOrderRepository.CreateCustomerOrderAsync(customerOrder);
        }
    }
}
