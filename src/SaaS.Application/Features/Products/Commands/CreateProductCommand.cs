﻿using MediatR;
using SaaS.Application.Interfaces;
using SaaS.Domain.Entities;

namespace SaaS.Application.Features.Products.Commands
{
    public record CreateProductCommand(string Name, decimal Price) : IRequest<Guid>;

	public class CreateProductHandler : IRequestHandler<CreateProductCommand, Guid>
	{
		private readonly IGenericRepository<Product> _repository;
        public CreateProductHandler(IGenericRepository<Product> repository) => _repository = repository;

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken ct)
        {
            var product = new Product { Name = request.Name, Price = request.Price };
            await _repository.AddAsync(product);
            return product.Id;
        }
	}
}
