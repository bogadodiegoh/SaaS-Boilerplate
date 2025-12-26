using MediatR;
using SaaS.Application.DTOs;
using SaaS.Application.Interfaces;
using SaaS.Domain.Entities;

namespace SaaS.Application.Features.Tenants.Queries
{
	public record GetAllTenantsQuery() : IRequest<IReadOnlyList<TenantDto>>;

	public class GetAllTenantsHandler : IRequestHandler<GetAllTenantsQuery, IReadOnlyList<TenantDto>>
	{
		private readonly IGenericRepository<Tenant> _repository;
		public GetAllTenantsHandler(IGenericRepository<Tenant> repository)
		{
			_repository = repository;
		}

		public async Task<IReadOnlyList<TenantDto>> Handle(GetAllTenantsQuery request, CancellationToken cancellationToken)
		{
			var tenants = await _repository.GetAllAsync();

			return tenants.Select(t => new TenantDto(
				t.Id, 
				t.Name, 
				t.Identifier, 
				t.IsActive)).ToList();
		}
	}
}
