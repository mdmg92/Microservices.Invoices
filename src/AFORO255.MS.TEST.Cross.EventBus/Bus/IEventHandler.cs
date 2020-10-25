using System.Threading.Tasks;
using Cross.EventBus.Events;

namespace Cross.EventBus.Bus
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
