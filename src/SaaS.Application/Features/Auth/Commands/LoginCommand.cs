using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SaaS.Application.DTOs;
using SaaS.Application.Interfaces;
using SaaS.Domain.Entities;

namespace SaaS.Application.Features.Auth.Commands
{
    public record LoginCommand(string Email, string Password) : IRequest<AuthResponse>;

    public class LoginHandler : IRequestHandler<LoginCommand, AuthResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IApplicationDbContext _context;

        public LoginHandler(
            UserManager<ApplicationUser> userManager, 
            IJwtTokenService jwtTokenService,
            IApplicationDbContext context)
        {
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
            _context = context;
        }

        public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken ct)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
        
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                throw new UnauthorizedAccessException("Invalid Credentials.");
            }

            var tenant = await _context.Tenants
                .FirstOrDefaultAsync(t => t.Identifier == user.TenantId, ct);

            if (tenant == null)
            {
                throw new UnauthorizedAccessException("Invalid organization.");
            }

            if (!tenant.IsActive)
            {
                throw new UnauthorizedAccessException("Your organization has been deactivated. Please contact support.");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenService.GenerateToken(user, roles);

            return new AuthResponse(
                token, 
                user.Email!, 
                user.FirstName, 
                user.LastName, 
                user.TenantId);
        }
    }
}
