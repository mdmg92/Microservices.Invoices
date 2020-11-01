using Microsoft.AspNetCore.Mvc;

namespace Pay.Controllers
{
    [Route("ping")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("ping");
    }
}