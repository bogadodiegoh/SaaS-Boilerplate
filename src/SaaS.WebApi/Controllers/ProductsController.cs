﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using SaaS.Application.Features.Products.Commands;
using SaaS.Application.Features.Products.Queries;

namespace SaaS.WebApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductsController : ControllerBase
	{
		private readonly IMediator _mediator;
        public ProductsController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommand command) => Ok(await _mediator.Send(command));

        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            var products = await _mediator.Send(new GetProductsQuery());
            return Ok(products);
        }
	}
}
