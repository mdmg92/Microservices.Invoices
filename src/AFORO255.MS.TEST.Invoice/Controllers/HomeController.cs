using Microsoft.AspNetCore.Mvc;

namespace Invoices.Controllers
{
    [Route("ping")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("ping");
    }
}