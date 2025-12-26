﻿using System.Linq.Expressions;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using SaaS.Application.Interfaces;
using SaaS.Domain.Common.Interfaces;
using SaaS.Domain.Entities;

namespace SaaS.Infrastructure.Persistence;

public class ApplicationDbContext: DbContext
{
	protected ITenantService TenantService { get; }

	public ApplicationDbContext(
		DbContextOptions<ApplicationDbContext> options,
		ITenantService tenantService) : base(options)
	{
		TenantService = tenantService;
	}

	public DbSet<Tenant> Tenants => Set<Tenant>();
	public DbSet<Product> Products => Set<Product>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Tenant>(entity =>
		{
			entity.HasKey(e => e.Id);
			entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
			entity.HasIndex(e => e.Identifier).IsUnique();
		});

		foreach (var entityType in modelBuilder.Model.GetEntityTypes())
		{
			if (typeof(IMustHaveTenant).IsAssignableFrom(entityType.ClrType))
			{
				modelBuilder.Entity(entityType.ClrType).HasQueryFilter(
					GenerateQueryFilterLambda(entityType.ClrType));
			}
		}

		modelBuilder.Entity<Product>(entity =>
		{
			entity.HasKey(e => e.Id);
			entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
			entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
		});
	}

	private LambdaExpression GenerateQueryFilterLambda(Type type)
	{
		var parameter = Expression.Parameter(type, "e");
		var property = Expression.Property(parameter, nameof(IMustHaveTenant.TenantId));
		var tenantId = Expression.Property(Expression.Constant(this), nameof(TenantService));
		var currentTenantId = Expression.Property(tenantId, nameof(ITenantService.TenantId));
		var comparison = Expression.Equal(property, currentTenantId);

		return Expression.Lambda(comparison, parameter);
	}

	public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		foreach (var entry in ChangeTracker.Entries<IMustHaveTenant>())
		{
			switch (entry.State)
			{
				case EntityState.Added:
					entry.Entity.TenantId = TenantService.TenantId 
						?? throw new Exception("Cannot create a record without a valid TenantId.");
					break;
			}
		}

		return base.SaveChangesAsync(cancellationToken);
	}
}
