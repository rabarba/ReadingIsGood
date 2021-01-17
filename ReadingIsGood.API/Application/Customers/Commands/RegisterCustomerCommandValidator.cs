using FluentValidation;

namespace ReadingIsGood.API.Application.Customers.Commands
{
    public class RegisterCustomerCommandValidator : AbstractValidator<RegisterCustomerCommand>
    {
        public RegisterCustomerCommandValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Customer Name should not be empty.");
            RuleFor(x => x.Email).NotNull().EmailAddress().WithMessage("Customer Email should not be empty and should be email format."); ;
            RuleFor(x => x.Address).NotNull().WithMessage("Customer Address should not be empty."); ;
            RuleFor(x => x.Phone).NotNull().WithMessage("Customer Phone should not be empty.");
        }
    }
}
