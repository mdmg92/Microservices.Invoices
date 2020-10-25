using Cross.EventBus.Events;

namespace Pay.Payments.Events
{
    public class InvoicePaymentAcceptedEvent : Event
    {
        public int Id { get; set; }

        public InvoicePaymentAcceptedEvent(int id)
        {
            Id = id;
        }
    }
}