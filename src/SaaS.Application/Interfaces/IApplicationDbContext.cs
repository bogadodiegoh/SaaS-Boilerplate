using Microsoft.EntityFrameworkCore;
using SaaS.Domain.Entities;

namespace SaaS.Application.Interfaces
{
	public interface IApplicationDbContext
    {
        DbSet<Tenant> Tenants { get; }
        DbSet<Product> Products { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
