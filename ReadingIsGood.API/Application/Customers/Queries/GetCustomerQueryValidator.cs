using FluentValidation;

namespace ReadingIsGood.API.Application.Customers.Queries
{
    public class GetCustomerQueryValidator : AbstractValidator<GetCustomerQuery>
    {
        public GetCustomerQueryValidator()
        {
            RuleFor(x => x.CustomerId).NotNull().WithMessage("Customer Id should not be empty.");
        }
    }
}
