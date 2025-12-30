using Microsoft.EntityFrameworkCore;
using SaaS.Domain.Entities;

namespace SaaS.Application.Interfaces
{
	public interface IApplicationDbContext
    {
        DbSet<Tenant> Tenants { get; }
        DbSet<Product> Products { get; }
        DbSet<Customer> Customers { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
