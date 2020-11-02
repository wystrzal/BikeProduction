using Delivery.Application.Commands;
using Delivery.Application.Queries;
using Delivery.Core.SearchSpecification;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Delivery.API.Controllers
{
    [Authorize(Policy = "Admin")]
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

        [HttpGet("pack/{packId}")]
        public async Task<IActionResult> GetPack(int packId)
        {
            try
            {
                return Ok(await mediator.Send(new GetPackQuery(packId)));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("packs")]
        public async Task<IActionResult> GetPacks([FromQuery] OrderFilteringData filteringData)
        {
            return Ok(await mediator.Send(new GetPacksQuery(filteringData)));
        }

        [HttpGet("loadingPlaces")]
        public async Task<IActionResult> GetLoadingPlaces([FromQuery] LoadingPlaceFilteringData filteringData)
        {
            return Ok(await mediator.Send(new GetLoadingPlacesQuery(filteringData)));
        }

        [HttpGet("loadingPlace/{loadingPlaceId}")]
        public async Task<IActionResult> GetLoadingPlace(int loadingPlaceId)
        {
            try
            {
                return Ok(await mediator.Send(new GetLoadingPlaceQuery(loadingPlaceId)));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("loading/{loadingPlaceId}/pack/{packId}")]
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
        
        [HttpPost("loadingPlace")]
        public async Task<IActionResult> AddLoadingPlace(AddLoadingPlaceCommand command)
        {
            try
            {
                await mediator.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("loadingPlace")]
        public async Task<IActionResult> UpdateLoadingPlace(UpdateLoadingPlaceCommand command)
        {
            try
            {
                await mediator.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("loadingPlace/{loadingPlaceId}")]
        public async Task<IActionResult> DeleteLoadingPlace(int loadingPlaceId)
        {
            try
            {
                await mediator.Send(new DeleteLoadingPlaceCommand(loadingPlaceId));
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