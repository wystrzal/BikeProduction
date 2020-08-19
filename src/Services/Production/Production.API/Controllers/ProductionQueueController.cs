using MediatR;
using Microsoft.AspNetCore.Mvc;
using Production.Application.Commands;
using Production.Application.Queries;
using System;
using System.Threading.Tasks;

namespace Production.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionQueueController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductionQueueController(IMediator mediator)
        {
            this.mediator = mediator;
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
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("finish/{id}")]
        public async Task<IActionResult> FinishProduction(int id)
        {
            try
            {
                await mediator.Send(new FinishProductionCommand(id));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProductionQueues()
        {
            return Ok(await mediator.Send(new GetProductionQueuesQuery()));
        }
    }
}