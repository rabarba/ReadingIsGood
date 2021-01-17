using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadingIsGood.API.Application.CustomerOrders.Commands;
using ReadingIsGood.API.Application.CustomerOrders.Queries;
using ReadingIsGood.API.Models;
using System.Net;
using System.Threading.Tasks;

namespace ReadingIsGood.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/customers")]
    public class CustomerOrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerOrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("{customerId}/orders")]
        [ProducesResponseType(typeof(HttpServiceResponseBase<string>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Post([FromRoute] string customerId, [FromBody] PlaceCustomerOrderCommand command)
        {
            command.CustomerId = customerId;
            var result = await _mediator.Send(command);

            return Created(string.Empty, new HttpServiceResponseBase<string> { Data = result, Code = HttpStatusCode.Created });
        }

        [HttpGet]
        [Route("{customerId}/orders")]
        [ProducesResponseType(typeof(HttpServiceResponseBase<OrderDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get([FromQuery] GetCustomerOrdersQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(new HttpServiceResponseBase<OrderDto> { Data = result, Code = HttpStatusCode.OK });
        }

        [HttpGet]
        [Route("{customerId}/orders/{orderId}")]
        [ProducesResponseType(typeof(HttpServiceResponseBase<OrderDetailDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get([FromQuery] GetCustomerOrderDetailQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(new HttpServiceResponseBase<OrderDetailDto> { Data = result, Code = HttpStatusCode.OK });
        }
    }
}
