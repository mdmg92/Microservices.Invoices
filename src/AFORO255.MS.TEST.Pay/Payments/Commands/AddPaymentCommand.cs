using System;
using System.Threading;
using System.Threading.Tasks;
using Cross.EventBus.Bus;
using MediatR;
using Pay.Data;
using Pay.Models;
using Pay.Payments.Events;

namespace Pay.Payments.Commands
{
    public class AddPaymentCommand : IRequest<int>
    {
        public int InvoiceId { get; set; }
        public decimal Amount { get; set; }

        public class AddPaymentCommandHandler : IRequestHandler<AddPaymentCommand, int>
        {
            private readonly PaymentDbContext _context;
            private readonly IEventBus _bus;

            public AddPaymentCommandHandler(PaymentDbContext context, IEventBus bus)
            {
                _context = context;
                _bus = bus;
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

                _bus.Publish(new InvoicePaymentAcceptedEvent(operation.InvoiceId));

                return operation.Id;
            }
        }
    }
}
