using Delivery.Application.Commands;
using Delivery.Application.Queries;
using Delivery.Core.SearchSpecification;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Delivery.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ILogger<DeliveryController> logger;

        public DeliveryController(IMediator mediator, ILogger<DeliveryController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        [HttpGet("packs")]
        public async Task<IActionResult> GetPacks([FromQuery] FilteringData filteringData)
        {
            return Ok(await mediator.Send(new GetPacksQuery(filteringData)));
        }

        [HttpPost("loading/{loadingPlaceId}/pack/{packId})")]
        public async Task<IActionResult> LoadPack(int loadingPlaceId, int packId)
        {
            try
            {
                await mediator.Send(new LoadPackCommand(loadingPlaceId, packId));
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("start/{loadingPlaceId}")]
        public async Task<IActionResult> StartDelivery(int loadingPlaceId)
        {
            try
            {
                await mediator.Send(new StartDeliveryCommand(loadingPlaceId));
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("complete/{loadingPlaceId}")]
        public async Task<IActionResult> CompleteDelivery(int loadingPlaceId)
        {
            try
            {
                await mediator.Send(new CompleteDeliveryCommand(loadingPlaceId));
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }


    }
}