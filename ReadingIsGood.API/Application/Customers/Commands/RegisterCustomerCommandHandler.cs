using MediatR;
using ReadingIsGood.Domain.Documents;
using ReadingIsGood.Domain.Events;
using ReadingIsGood.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace ReadingIsGood.API.Application.Customers.Commands
{
    public class RegisterCustomerCommandHandler : IRequestHandler<RegisterCustomerCommand, string>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMediator _mediator;
        public RegisterCustomerCommandHandler(ICustomerRepository customerRepository, IMediator mediator)
        {
            _customerRepository = customerRepository;
            _mediator = mediator;
        }

        public async Task<string> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                Name = request.Name,
                Address = request.Address,
                Email = request.Email,
                Phone = request.Phone
            };

            var customerId = await _customerRepository.CreateCustomerAsync(customer);

            _ = _mediator.Publish(new CustomerCreatedEvent
            {
                CustomerId = customerId
            }, cancellationToken);

            return customerId;
        }
    }
}
