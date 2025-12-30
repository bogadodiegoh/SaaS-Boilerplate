using System.Security.Claims;
using SaaS.Application.Interfaces;
using SaaS.Domain.Constants;

namespace SaaS.WebApi.Services
{
	public class TenantService : ITenantService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public TenantService(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public string? TenantId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(CustomClaimTypes.TenantId);

		public string? GetTenantId()
		{
			var tenantId = _httpContextAccessor.HttpContext?.User?.FindFirst("tenantId")?.Value;
            
            return tenantId;
		}

		public bool HasTenant() => !string.IsNullOrEmpty(TenantId);
	}
}
