using System.Threading.Tasks;
using AFORO255.MS.TEST.Cross.Kafka.Events;

namespace AFORO255.MS.TEST.Cross.Kafka.Bus
{
    public interface IEventBus
    {
        void Publish<T>(T @event) where T : Event, new();

        void Subscribe<T, TH>()
            where T : Event
            where TH : IEventHandler<T>;

        Task HandleMessage<T>(string eventName, T message);
    }
}
