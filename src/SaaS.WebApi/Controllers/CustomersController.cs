using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaaS.Application.DTOs;
using SaaS.Application.Features.Customers.Commands;
using SaaS.Application.Features.Customers.Queries;

namespace SaaS.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomersController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateCustomerCommand command) 
            => Ok(await _mediator.Send(command));

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CustomerDto>>> GetAll() 
            => Ok(await _mediator.Send(new GetCustomersQuery()));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteCustomerCommand(id));
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCustomerCommand command)
        {
            if (id != command.Id) return BadRequest("ID mismatch");
    
            var result = await _mediator.Send(command);
            if (!result) return NotFound();
    
            return NoContent();
        }
    }
}
