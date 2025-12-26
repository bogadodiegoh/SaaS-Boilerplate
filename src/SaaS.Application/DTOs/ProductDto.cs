﻿namespace SaaS.Application.DTOs
{
	public record ProductDto(Guid Id, string Name, decimal Price, string TenantId);
}
