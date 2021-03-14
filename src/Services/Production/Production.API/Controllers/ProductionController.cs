using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Production.Application.Commands;
using Production.Application.Queries;
using Production.Core.Models;
using System;
using System.Threading.Tasks;

namespace Production.API.Controllers
{
    [Authorize(Policy = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductionController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("confirm/{id}")]
        public async Task<IActionResult> ConfirmProduction(int id)
        {
            await mediator.Send(new ConfirmProductionCommand(id));
            return Ok();
        }

        [HttpPost("start/{id}")]
        public async Task<IActionResult> StartCreatingProducts(int id)
        {
            await mediator.Send(new StartCreatingProductsCommand(id));
            return Ok();
        }

        [HttpPost("finish/{id}")]
        public async Task<IActionResult> FinishProduction(int id)
        {
            var token = Request.Headers[HeaderNames.Authorization].ToString().Substring(7);
            await mediator.Send(new FinishProductionCommand(id, token));
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetProductionQueues([FromQuery] ProductionQueueFilteringData filteringData)
        {
            return Ok(await mediator.Send(new GetProductionQueuesQuery(filteringData)));
        }
    }
}