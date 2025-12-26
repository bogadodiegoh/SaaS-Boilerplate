﻿using Microsoft.EntityFrameworkCore;
using SaaS.Application.Interfaces;
using SaaS.Domain.Common;

namespace SaaS.Infrastructure.Persistence.Repositories
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		protected readonly ApplicationDbContext _context;

		public GenericRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<T> AddAsync(T entity)
		{
			await _context.Set<T>().AddAsync(entity);
			await _context.SaveChangesAsync();
			return entity;
		}

		public async Task DeleteAsync(T entity)
		{
			_context.Set<T>().Remove(entity);
			await _context.SaveChangesAsync();
		}

		public async Task<T?> GetByIdAsync(Guid id)
		{
			return await _context.Set<T>().FindAsync(id);
		}

		public async Task<IReadOnlyList<T>> GetAllAsync()
		{
			return await _context.Set<T>().ToListAsync();
		}

		public async Task UpdateAsync(T entity)
		{
			_context.Entry(entity).State = EntityState.Modified;
			entity.UpdatedAt = DateTime.UtcNow;
			await _context.SaveChangesAsync();
		}
	}
}
