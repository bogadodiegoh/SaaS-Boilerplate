﻿using MediatR;
using SaaS.Application.DTOs;
using SaaS.Application.Interfaces;
using SaaS.Domain.Entities;

namespace SaaS.Application.Features.Products.Queries
{
	public record GetProductsQuery() : IRequest<List<ProductDto>>;
	public class GetProductsHandler : IRequestHandler<GetProductsQuery, List<ProductDto>>
	{
		private readonly IGenericRepository<Product> _repository;

        public GetProductsHandler(IGenericRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<List<ProductDto>> Handle(GetProductsQuery request, CancellationToken ct)
        {
            var products = await _repository.GetAllAsync();

            return products.Select(p => new ProductDto(
                p.Id, 
                p.Name, 
                p.Price, 
                p.TenantId)).ToList();
        }
	}
}
