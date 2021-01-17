using FluentValidation;

namespace ReadingIsGood.API.Application.CustomerOrders.Commands
{
    public class PlaceCustomerOrderCommandValidator : AbstractValidator<PlaceCustomerOrderCommand>
    {
        public PlaceCustomerOrderCommandValidator()
        {
            RuleFor(x => x.CustomerId).NotNull().WithMessage("Customer Id should not be empty.");
            RuleFor(x => x.Products).NotNull().Must(products => products.Count > 0).WithMessage("Products should not be empty");
        }
    }
}
