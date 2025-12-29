﻿namespace SaaS.Application.Interfaces
{
	public interface ITenantService
	{
		string? TenantId { get; }
		bool HasTenant();
	}
}
