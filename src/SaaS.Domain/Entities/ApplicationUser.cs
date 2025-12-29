using Microsoft.AspNetCore.Identity;

namespace SaaS.Domain.Entities
{
	public class ApplicationUser : IdentityUser<Guid>
	{
		public string FirstName { get; set; } = default!;
		public string LastName { get; set; } = default!;
		public string TenantId { get; set; } = default!;
	}
}
