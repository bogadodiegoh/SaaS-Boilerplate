using MediatR;
using Microsoft.AspNetCore.Identity;
using SaaS.Application.Interfaces;
using SaaS.Domain.Entities;
using SaaS.Domain.Constants;
using Microsoft.EntityFrameworkCore;
using SaaS.Domain.Exceptions;

namespace SaaS.Application.Features.Auth.Commands
{
    public record RegisterTenantCommand(
        string Email, 
        string Password, 
        string FirstName, 
        string LastName, 
        string TenantId, 
        string CompanyName) : IRequest<bool>;

    public class RegisterTenantHandler : IRequestHandler<RegisterTenantCommand, bool>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationDbContext _context;

        public RegisterTenantHandler(UserManager<ApplicationUser> userManager, IApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
		}

        public async Task<bool> Handle(RegisterTenantCommand request, CancellationToken ct)
        {
            var existingTenant = await _context.Tenants
                .AnyAsync(t => t.Identifier == request.TenantId, ct);

            if (existingTenant)
            {
                throw new TenantAlreadyExistsException(request.TenantId);
            }

            var tenant = new Tenant
            {
                Identifier = request.TenantId,
                Name = request.CompanyName,
                IsActive = true
            };
            _context.Tenants.Add(tenant);

            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                TenantId = request.TenantId,
                EmailConfirmed = true 
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, AppRoles.Admin);
                await _context.SaveChangesAsync(ct);
                return true;
            }

            return false;
        }
    }
}
