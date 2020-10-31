using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invoices.Invoices.Queries;
using Invoices.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Invoices.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<InvoicesController> _logger;

        public InvoicesController(IMediator mediator, ILogger<InvoicesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("{customer:int}")]
        public async Task<ActionResult<IEnumerable<Invoice>>> Get(int customer)
        {
            return Ok(await _mediator.Send(new GetInvoicesByCustomer {Id = customer}));
        }
    }
}
