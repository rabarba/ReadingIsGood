using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadingIsGood.API.Application.Customers.Commands;
using ReadingIsGood.API.Application.Customers.Queries;
using ReadingIsGood.API.Models;
using System.Net;
using System.Threading.Tasks;

namespace ReadingIsGood.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/customers")]
    [ProducesResponseType(typeof(HttpServiceResponseBase), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(HttpServiceResponseBase), (int)HttpStatusCode.InternalServerError)]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(HttpServiceResponseBase<string>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Post([FromBody] RegisterCustomerCommand command)
        {
            var result = await _mediator.Send(command);
            return Created(string.Empty, new HttpServiceResponseBase<string> { Data = result, Code = HttpStatusCode.Created });
        }

        [HttpGet("{customerId}")]
        [ProducesResponseType(typeof(HttpServiceResponseBase<CustomerDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get([FromQuery] GetCustomerQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(new HttpServiceResponseBase<CustomerDto> { Data = result, Code = HttpStatusCode.OK });
        }
    }
}
