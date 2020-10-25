using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Driver;
using Transactions.Data;
using Transactions.Models;

namespace Transactions.Transactions.Queries
{
    public class GetTransactionsByInvoiceId : IRequest<IEnumerable<Transaction>>
    {
        public int Id { get; set; }

        public class GetTransactionsByInvoiceIdQueryHandler : IRequestHandler<GetTransactionsByInvoiceId,
                IEnumerable<Transaction>>
        {
            private readonly ITransactionsDbContext _context;

            public GetTransactionsByInvoiceIdQueryHandler(ITransactionsDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Transaction>> Handle(GetTransactionsByInvoiceId query, CancellationToken cancellationToken)
            {
                var transactionsQuery = await _context.Transactions
                    .FindAsync(t => t.InvoiceId == query.Id);

                var transactions = await transactionsQuery.ToListAsync();

                return transactions;
            }
        }
    }
}