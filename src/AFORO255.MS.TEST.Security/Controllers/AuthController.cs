using System.Threading.Tasks;
using Cross.Jwt;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Security.Token;

namespace Security.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly JwtOptions _configuration;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IMediator mediator, IOptionsSnapshot<JwtOptions> configuration, ILogger<AuthController> logger)
        {
            _mediator = mediator;
            _configuration = configuration.Value;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AuthenticationRequest authRequest)
        {
            if (await _mediator.Send(authRequest))
            {
                Response.Headers.Add("access-control-expose-headers", "Authorization");
                Response.Headers.Add("Authorization", $"Bearer {JwtToken.Create(_configuration)}");

                return Ok();
            }

            return Unauthorized();
        }
    }
}
