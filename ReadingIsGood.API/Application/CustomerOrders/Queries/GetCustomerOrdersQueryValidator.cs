using FluentValidation;

namespace ReadingIsGood.API.Application.CustomerOrders.Queries
{
    public class GetCustomerOrdersQueryValidator : AbstractValidator<GetCustomerOrdersQuery>
    {
        public GetCustomerOrdersQueryValidator()
        {
            RuleFor(x => x.CustomerId).NotNull().WithMessage("Customer Id should not be empty.");
        }
    }
}
