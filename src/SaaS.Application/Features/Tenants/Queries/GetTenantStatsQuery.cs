using MediatR;
using SaaS.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SaaS.Application.Features.Tenants.Queries;

public record GetTenantStatsQuery : IRequest<TenantStatsDto>;

public class TenantStatsDto
{
    public int TotalCustomers { get; set; }
    public DateTime LastCustomerAdded { get; set; }
}

public class GetTenantStatsHandler : IRequestHandler<GetTenantStatsQuery, TenantStatsDto>
{
    private readonly IApplicationDbContext _context;
    public GetTenantStatsHandler(IApplicationDbContext context) => _context = context;

    public async Task<TenantStatsDto> Handle(GetTenantStatsQuery request, CancellationToken ct)
    {
        var customers = await _context.Customers.ToListAsync(ct);
        
        return new TenantStatsDto
        {
            TotalCustomers = customers.Count,
            LastCustomerAdded = customers.Any() ? customers.Max(x => x.Id) > 0 ? DateTime.Now : DateTime.MinValue : DateTime.MinValue
        };
    }
}