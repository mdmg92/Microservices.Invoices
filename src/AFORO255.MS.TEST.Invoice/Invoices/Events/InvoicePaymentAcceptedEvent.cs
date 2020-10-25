using System;
using System.Threading.Tasks;
using Cross.EventBus.Bus;
using Cross.EventBus.Events;
using Invoices.Data;
using Microsoft.EntityFrameworkCore;

namespace Invoices.Invoices.Events
{
    public class InvoicePaymentAcceptedEvent : Event
    {
        public int Id { get; set; }

        public class InvoicePaymentAcceptedEventHandler : IEventHandler<InvoicePaymentAcceptedEvent>
        {
            private readonly InvoiceDbContext _context;

            public InvoicePaymentAcceptedEventHandler(InvoiceDbContext context)
            {
                _context = context;
            }

            public async Task Handle(InvoicePaymentAcceptedEvent @event)
            {
                var invoiceInDb = await _context.Invoices.FirstOrDefaultAsync(i => i.Id == @event.Id && i.State == 0);

                if (invoiceInDb is null)
                {
                    throw new ApplicationException("Invoice not found");
                }

                invoiceInDb.State = 1;
                _context.Invoices.Update(invoiceInDb);

                await _context.SaveChangesAsync();
            }
        }
    }
}