using MediatR;

namespace ReadingIsGood.API.Application.Customers.Commands
{
    public class RegisterCustomerCommand : IRequest<string>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
