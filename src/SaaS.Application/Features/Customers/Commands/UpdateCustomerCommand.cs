using MediatR;
using SaaS.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SaaS.Application.Features.Customers.Commands
{
    public record UpdateCustomerCommand(int Id, string Name, string Email, string Phone) : IRequest<bool>;

    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        public UpdateCustomerCommandHandler(IApplicationDbContext context) => _context = context;

        public async Task<bool> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (customer == null) return false;

            customer.Name = request.Name;
            customer.Email = request.Email;
            customer.Phone = request.Phone;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
