using MediatR;
using ReadingIsGood.Domain.Documents;
using ReadingIsGood.Domain.Events;
using ReadingIsGood.Domain.Exceptions;
using ReadingIsGood.Domain.Interfaces;
using System.Linq;
using System.Net;
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
            var validationResult = new RegisterCustomerCommandValidator().Validate(request);
            if (!validationResult.IsValid)
            {
                var message = string.Join(',', validationResult.Errors.Select(x => x.ErrorMessage).ToList());
                throw new ApiException(message, HttpStatusCode.BadRequest);
            }

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
