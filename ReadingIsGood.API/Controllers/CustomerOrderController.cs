using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReadingIsGood.API.Application.CustomerOrders.Commands;
using ReadingIsGood.API.Models;
using System.Net;
using System.Threading.Tasks;

namespace ReadingIsGood.API.Controllers
{
    [ApiController]
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
    }
}
