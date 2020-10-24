using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Invoices.Data;
using Invoices.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Invoices.Invoices.Queries
{
    public class GetInvoicesByCustomer : IRequest<IEnumerable<Invoice>>
    {
        public int Id { get; set; }

        public class GetInvoicesByCustomerQueryHandler : IRequestHandler<GetInvoicesByCustomer, IEnumerable<Invoice>>
        {
            private readonly InvoiceDbContext _context;

            public GetInvoicesByCustomerQueryHandler(InvoiceDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Invoice>> Handle(GetInvoicesByCustomer query, CancellationToken cancellationToken) 
                => await _context.Invoices.Where(i => i.CustomerId == query.Id).ToListAsync(cancellationToken: cancellationToken);
        }
    }
}