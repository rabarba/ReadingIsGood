using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ReadingIsGood.API.Application.Customers.Commands
{
    public class RegisterCustomerCommand : IRequest<string>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
    }
}
