using SaaS.Domain.Entities;

namespace SaaS.Application.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(ApplicationUser user, IList<string> roles);
}
