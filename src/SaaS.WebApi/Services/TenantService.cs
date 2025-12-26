﻿using SaaS.Application.Interfaces;

namespace SaaS.WebApi.Services
{
	public class TenantService : ITenantService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public TenantService(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public string? TenantId => _httpContextAccessor.HttpContext?.Request.Headers["x-tenant-id"].ToString();
	}
}
