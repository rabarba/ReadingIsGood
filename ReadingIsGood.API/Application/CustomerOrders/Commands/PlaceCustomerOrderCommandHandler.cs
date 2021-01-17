using MediatR;
using ReadingIsGood.Domain.Documents;
using ReadingIsGood.Domain.Events;
using ReadingIsGood.Domain.Exceptions;
using ReadingIsGood.Domain.Interfaces;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ReadingIsGood.API.Application.CustomerOrders.Commands
{
    public class PlaceCustomerOrderCommandHandler : IRequestHandler<PlaceCustomerOrderCommand, string>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerOrderRepository _customerOrderRepository;
        private readonly IMediator _mediator;

        public PlaceCustomerOrderCommandHandler(ICustomerOrderRepository customerOrderRepository, IMediator mediator, ICustomerRepository customerRepository)
        {
            _customerOrderRepository = customerOrderRepository;
            _mediator = mediator;
            _customerRepository = customerRepository;
        }

        public async Task<string> Handle(PlaceCustomerOrderCommand request, CancellationToken cancellationToken)
        {
            var validationResult = new PlaceCustomerOrderCommandValidator().Validate(request);
            if (!validationResult.IsValid)
            {
                var message = string.Join(',', validationResult.Errors.Select(x => x.ErrorMessage).ToList());
                throw new ApiException(message, HttpStatusCode.BadRequest);
            }

            var customer = await _customerRepository.GetCustomerAsync(request.CustomerId);
            if (customer == null)
            {
                throw new ApiException("Customer not found", HttpStatusCode.BadRequest);
            }

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
