﻿using MediatR;
using SaaS.Application.DTOs;
using SaaS.Application.Interfaces;
using SaaS.Domain.Entities;

namespace SaaS.Application.Features.Tenants.Commands
{
	public record CreateTenantCommand(string Name, string Identifier) : IRequest<TenantDto>;

	public class CreateTenantHandler : IRequestHandler<CreateTenantCommand, TenantDto>
	{
		private readonly IGenericRepository<Tenant> _repository;

		public CreateTenantHandler(IGenericRepository<Tenant> repository)
		{
			_repository = repository;
		}

		public async Task<TenantDto> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
		{
			var tenant = new Tenant
			{
				Name = request.Name,
				Identifier = request.Identifier
			};

			await _repository.AddAsync(tenant);

			return new TenantDto(tenant.Id, tenant.Name, tenant.Identifier, tenant.IsActive);
		}
	}
}
