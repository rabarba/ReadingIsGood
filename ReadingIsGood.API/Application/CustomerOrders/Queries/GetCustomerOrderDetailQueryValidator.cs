using FluentValidation;

namespace ReadingIsGood.API.Application.CustomerOrders.Queries
{
    public class GetCustomerOrderDetailQueryValidator : AbstractValidator<GetCustomerOrderDetailQuery>
    {
        public GetCustomerOrderDetailQueryValidator()
        {
            RuleFor(x => x.CustomerId).NotNull().WithMessage("Customer Id should not be empty.");
            RuleFor(x => x.OrderId).NotNull().WithMessage("Customer Order Id should not be empty.");
        }
    }
}
