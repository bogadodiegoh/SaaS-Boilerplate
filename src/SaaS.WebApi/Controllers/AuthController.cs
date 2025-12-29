using MediatR;
using Microsoft.AspNetCore.Mvc;
using SaaS.Application.Features.Auth.Commands;

namespace SaaS.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator) => _mediator = mediator;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

    [HttpPost("register-tenant")]
    public async Task<IActionResult> RegisterTenant([FromBody] RegisterTenantCommand command)
    {
        var result = await _mediator.Send(command);
        return result ? Ok("Tenant registered successfully.") : BadRequest();
    }
}
