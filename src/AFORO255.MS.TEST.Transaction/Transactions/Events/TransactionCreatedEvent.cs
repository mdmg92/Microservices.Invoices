using System;
using System.Threading.Tasks;
using Cross.EventBus.Bus;
using Cross.EventBus.Events;
using Transactions.Data;
using Transactions.Models;

namespace Transactions.Transactions.Events
{
    public class TransactionCreatedEvent : Event
    {
        public int InvoiceId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public class TransactionCreatedEventHandler : IEventHandler<TransactionCreatedEvent>
        {
            private readonly ITransactionsDbContext _context;

            public TransactionCreatedEventHandler(ITransactionsDbContext context)
            {
                _context = context;
            }
            
            public async Task Handle(TransactionCreatedEvent @event)
            {
                var transaction = new Transaction
                {
                    InvoiceId = @event.InvoiceId,
                    Amount = @event.Amount,
                    Date = @event.Date
                };
                
                await _context.Transactions.InsertOneAsync(transaction);
            }
        }
    }
}
