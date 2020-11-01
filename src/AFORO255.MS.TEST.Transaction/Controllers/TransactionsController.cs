using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Transactions.Models;
using Transactions.Transactions.Queries;

namespace Transactions.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<TransactionsController> _logger;

        public TransactionsController(IMediator mediator, ILogger<TransactionsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("{invoice:int}")]
        public async Task<ActionResult<IEnumerable<Transaction>>> Get(int invoice)
        {
            return Ok(await _mediator.Send(new GetTransactionsByInvoiceId { Id = invoice }));
        }
    }
}