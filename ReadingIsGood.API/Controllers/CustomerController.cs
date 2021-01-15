using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReadingIsGood.API.Application.Customers.Commands;
using ReadingIsGood.API.Application.Customers.Queries;
using System.Net;
using System.Threading.Tasks;

namespace ReadingIsGood.API.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Post([FromBody] RegisterCustomerCommand command)
        {
            var result = await _mediator.Send(command);
            return Created(string.Empty, result);
        }

        [HttpGet("{CustomerId}")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get([FromQuery] GetCustomerQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
