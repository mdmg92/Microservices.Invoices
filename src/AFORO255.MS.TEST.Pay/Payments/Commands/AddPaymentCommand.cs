using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Pay.Data;
using Pay.Models;

namespace Pay.Payments.Commands
{
    public class AddPaymentCommand : IRequest<int>
    {
        public int InvoiceId { get; set; }
        public decimal Amount { get; set; }

        public class AddPaymentCommandHandler : IRequestHandler<AddPaymentCommand, int>
        {
            private readonly PaymentDbContext _context;

            public AddPaymentCommandHandler(PaymentDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(AddPaymentCommand command, CancellationToken cancellationToken)
            {
                var operation = new Operation
                {
                    Amount = command.Amount,
                    InvoiceId = command.InvoiceId,
                    Date = DateTime.Now
                };

                _context.Operations.Add(operation);
                await _context.SaveChangesAsync(cancellationToken);

                return operation.Id;
            }
        }
    }
}
