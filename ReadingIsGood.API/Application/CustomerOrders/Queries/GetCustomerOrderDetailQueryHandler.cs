using MediatR;
using ReadingIsGood.Domain.Exceptions;
using ReadingIsGood.Domain.Interfaces;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ReadingIsGood.API.Application.CustomerOrders.Queries
{
    public class GetCustomerOrderDetailQueryHandler : IRequestHandler<GetCustomerOrderDetailQuery, OrderDetailDto>
    {
        private readonly ICustomerOrderRepository _customerOrderRepository;
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerOrderDetailQueryHandler(ICustomerOrderRepository customerOrderRepository, ICustomerRepository customerRepository)
        {
            _customerOrderRepository = customerOrderRepository;
            _customerRepository = customerRepository;
        }
        public async Task<OrderDetailDto> Handle(GetCustomerOrderDetailQuery request, CancellationToken cancellationToken)
        {
            var validationResult = new GetCustomerOrderDetailQueryValidator().Validate(request);
            if (!validationResult.IsValid)
            {
                var message = string.Join(',', validationResult.Errors.Select(x => x.ErrorMessage).ToList());
                throw new ApiException(message, HttpStatusCode.BadRequest);
            }

            var orderDetail = _customerOrderRepository.GetCustomerOrderDetailAsync(request.OrderId);
            var customer = _customerRepository.GetCustomerAsync(request.CustomerId);

            await Task.WhenAll(orderDetail, customer);

            if (customer == null)
            {
                throw new ApiException("Customer not found", System.Net.HttpStatusCode.BadRequest);
            }

            if (orderDetail == null)
            {
                throw new ApiException("Order Detail not found", System.Net.HttpStatusCode.BadRequest);
            }

            return new OrderDetailDto
            {
                CustomerId = customer.Result.Id,
                CustomerName = customer.Result.Name,
                CustomerEmail = customer.Result.Email,
                CustomerPhone = customer.Result.Phone,
                CustomerOrderId = orderDetail.Result.Id,
                CreatedAt = orderDetail.Result.CreatedAt,
                TotalPrice = orderDetail.Result.TotalPrice,
                Products = orderDetail.Result.Products.Select(x => new OrderDetailProductDto
                {
                    Id = x.Id,
                    Price = x.Price,
                    Quantity = x.Quantity
                }).ToList()
            };
        }
    }
}
