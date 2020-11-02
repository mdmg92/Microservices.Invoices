using System;
using AFORO255.MS.TEST.Cross.Kafka.Bus;
using AFORO255.MS.TEST.Cross.Kafka.Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AFORO255.MS.TEST.Producer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IEventBus _kafka;

        public HomeController(ILogger<HomeController> logger, IEventBus kafka)
        {
            _logger = logger;
            _kafka = kafka;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var miMensaje = new MiMensaje
            {
                Titulo = "Test",
                Mensaje = "Kafka Mensaje"
            };
            _kafka.Publish(miMensaje);

            return Ok($"Publicado {JsonConvert.SerializeObject(miMensaje)}");
        }
    }

    public class MiMensaje : Event
    {
        public string Titulo { get; set; }
        public string Mensaje { get; set; }
    }
    
}