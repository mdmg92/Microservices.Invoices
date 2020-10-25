﻿using System.Threading.Tasks;
using Cross.EventBus.Events;

namespace Cross.EventBus.Bus
{
    public interface IEventBus
    {
        void Publish<T>(T @event) where T : Event;

        void Subscribe<T, TH>()
            where T : Event
            where TH : IEventHandler<T>;
    }
}
