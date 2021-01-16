using MediatR;
using ReadingIsGood.Domain.Documents;
using ReadingIsGood.Domain.Events;
using ReadingIsGood.Domain.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ReadingIsGood.API.Application.CustomerOrders.Commands
{
    public class PlaceCustomerOrderCommandHandler : IRequestHandler<PlaceCustomerOrderCommand, string>
    {
        private readonly ICustomerOrderRepository _customerOrderRepository;
        private readonly IMediator _mediator;

        public PlaceCustomerOrderCommandHandler(ICustomerOrderRepository customerOrderRepository, IMediator mediator)
        {
            _customerOrderRepository = customerOrderRepository;
            _mediator = mediator;
        }

        public async Task<string> Handle(PlaceCustomerOrderCommand request, CancellationToken cancellationToken)
        {
            var customerOrder = new CustomerOrder
            {
                CustomerId = request.CustomerId,
                Products = request.Products.Select(x => new OrderProduct
                {
                    Id = x.Id,
                    Quantity = x.Quantity
                }).ToList()
            };

            var result = await _customerOrderRepository.CreateCustomerOrderAsync(customerOrder);

            _ = _mediator.Publish(new CustomerOrderPlacedEvent
            {
                CustomerOrderId = result.Id,
                OrderedProducts = result.Products,
                TotalPrice = result.TotalPrice
            });

            return result.Id;
        }
    }
}
