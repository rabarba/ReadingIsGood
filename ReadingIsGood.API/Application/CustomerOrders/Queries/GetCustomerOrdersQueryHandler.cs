using MediatR;
using ReadingIsGood.Domain.Exceptions;
using ReadingIsGood.Domain.Interfaces;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ReadingIsGood.API.Application.CustomerOrders.Queries
{
    public class GetCustomerOrdersQueryHandler : IRequestHandler<GetCustomerOrdersQuery, OrderDto>
    {
        private readonly ICustomerOrderRepository _customerOrderRepository;

        public GetCustomerOrdersQueryHandler(ICustomerOrderRepository customerOrderRepository)
        {
            _customerOrderRepository = customerOrderRepository;
        }
        public async Task<OrderDto> Handle(GetCustomerOrdersQuery request, CancellationToken cancellationToken)
        {
            var validationResult = new GetCustomerOrdersQueryValidator().Validate(request);
            if (!validationResult.IsValid)
            {
                var message = string.Join(',', validationResult.Errors.Select(x => x.ErrorMessage).ToList());
                throw new ApiException(message, HttpStatusCode.BadRequest);
            }

            var customerOrders = await _customerOrderRepository.GetCustomerOrdersAsync(request.CustomerId);
            return new OrderDto
            {
                Orders = customerOrders.Select(x => new OrderDtoItem
                {
                    Id = x.Id,
                    TotalPrice = x.TotalPrice,
                    CreatedAt = x.CreatedAt
                }).ToList()
            };
        }
    }
}
