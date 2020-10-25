using System;
using MediatR;

namespace Cross.EventBus.Commands
{
    public abstract class Command<T> : IRequest<T>
    {
        public string MessageType { get; protected set; }
        public DateTime Timestamp { get; protected set; }

        protected Command()
        {
            MessageType = GetType().Name;
            Timestamp = DateTime.Now;
        }
    }
}
