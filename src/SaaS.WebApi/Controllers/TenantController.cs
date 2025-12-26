﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using SaaS.Application.DTOs;
using SaaS.Application.Features.Tenants.Commands;
using SaaS.Application.Features.Tenants.Queries;

namespace SaaS.WebApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TenantsController : ControllerBase
	{
		private readonly IMediator _mediator;

		public TenantsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		public async Task<ActionResult<TenantDto>> Create(CreateTenantCommand command)
		{
			var result = await _mediator.Send(command);
			return Ok(result);
		}

		[HttpGet]
		public async Task<ActionResult<IReadOnlyList<TenantDto>>> GetAll()
		{
			var result = await _mediator.Send(new GetAllTenantsQuery());
			return Ok(result);
		}
	}
}
