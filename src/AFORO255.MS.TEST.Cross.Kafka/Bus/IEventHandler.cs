using System.Threading.Tasks;
using AFORO255.MS.TEST.Cross.Kafka.Events;

namespace AFORO255.MS.TEST.Cross.Kafka.Bus
{
    public interface IEventHandler<in TEvent> : IEventHandler
         where TEvent : Event
    {
        Task Handle(TEvent @event);
    }

    public interface IEventHandler
    {
    }
}
