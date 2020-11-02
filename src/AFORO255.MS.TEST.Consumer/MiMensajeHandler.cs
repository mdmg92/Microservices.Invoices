using System;
using System.Threading.Tasks;
using AFORO255.MS.TEST.Cross.Kafka.Bus;
using AFORO255.MS.TEST.Cross.Kafka.Events;
using Newtonsoft.Json;

namespace AFORO255.MS.TEST.Consumer
{
    public class MiMensaje : Event
    {
        public string Titulo { get; set; }
        public string Mensaje { get; set; }

        public class MiMensajeHandler : IEventHandler<MiMensaje>
        {
            public Task Handle(MiMensaje @event)
            {
                Console.WriteLine($"Mensaje recibido: {JsonConvert.SerializeObject(@event)}");
                
                return Task.CompletedTask;
            }
        }
    }
}