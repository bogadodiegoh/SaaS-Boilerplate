using MediatR;
using SaaS.Application.DTOs;
using SaaS.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SaaS.Application.Features.Customers.Queries
{
    public record GetCustomersQuery() : IRequest<IReadOnlyList<CustomerDto>>;

    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, IReadOnlyList<CustomerDto>>
    {
        private readonly IApplicationDbContext _context;
        public GetCustomersQueryHandler(IApplicationDbContext context) => _context = context;

        public async Task<IReadOnlyList<CustomerDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            return await _context.Customers
                .Select(c => new CustomerDto(c.Id, c.Name, c.Email, c.Phone))
                .ToListAsync(cancellationToken);
        }
    }
}
