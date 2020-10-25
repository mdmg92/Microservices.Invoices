using System;
using Cross.EventBus.Events;

namespace Pay.Payments.Events
{
    public class TransactionCreatedEvent : Event
    {
        public int InvoiceId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public TransactionCreatedEvent(int invoiceId, decimal amount)
        {
            InvoiceId = invoiceId;
            Amount = amount;
            Date = DateTime.Now;
        }
    }
}
