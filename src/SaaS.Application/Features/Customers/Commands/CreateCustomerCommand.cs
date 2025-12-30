using MediatR;
using SaaS.Application.Interfaces;
using SaaS.Domain.Entities;
using SaaS.Domain.Common;

namespace SaaS.Application.Features.Customers.Commands
{
    public record CreateCustomerCommand(string Name, string Email, string Phone) : IRequest<int>;

    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly ITenantService _tenantService;

        public CreateCustomerCommandHandler(IApplicationDbContext context, ITenantService tenantService)
        {
            _context = context;
            _tenantService = tenantService;
        }

        public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                TenantId = _tenantService.GetTenantId()!
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync(cancellationToken);
            return customer.Id;
        }
    }
}
