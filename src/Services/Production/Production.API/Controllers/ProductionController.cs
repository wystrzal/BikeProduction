using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Production.Application.Commands;
using Production.Application.Queries;
using Production.Core.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Production.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ILogger<ProductionController> logger;

        public ProductionController(IMediator mediator, ILogger<ProductionController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        [HttpPost("confirm/{id}")]
        public async Task<IActionResult> ConfirmProduction(int id)
        {
            try
            {
                await mediator.Send(new ConfirmProductionCommand(id));
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("start/{id}")]
        public async Task<IActionResult> StartCreatingProducts(int id)
        {
            try
            {
                await mediator.Send(new StartCreatingProductsCommand(id));
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("finish/{id}")]
        public async Task<IActionResult> FinishProduction(int id)
        {
            try
            {
                var token = Request.Headers[HeaderNames.Authorization].ToString().Substring(7);
                await mediator.Send(new FinishProductionCommand(id, token));
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProductionQueues([FromQuery] ProductionQueueFilteringData filteringData)
        {
            return Ok(await mediator.Send(new GetProductionQueuesQuery(filteringData)));
        }
    }
}