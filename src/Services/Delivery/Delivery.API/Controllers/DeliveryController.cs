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

        public DeliveryController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("pack/{packId}")]
        public async Task<IActionResult> GetPack(int packId)
        {
            return Ok(await mediator.Send(new GetPackQuery(packId)));
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
            return Ok(await mediator.Send(new GetLoadingPlaceQuery(loadingPlaceId)));
        }

        [HttpPost("loading/{loadingPlaceId}/pack/{packId}")]
        public async Task<IActionResult> LoadPack(int loadingPlaceId, int packId)
        {
            await mediator.Send(new LoadPackCommand(loadingPlaceId, packId));
            return Ok();
        }

        [HttpPost("start/{loadingPlaceId}")]
        public async Task<IActionResult> StartDelivery(int loadingPlaceId)
        {
            await mediator.Send(new StartDeliveryCommand(loadingPlaceId));
            return Ok();
        }

        [HttpPost("complete/{loadingPlaceId}")]
        public async Task<IActionResult> CompleteDelivery(int loadingPlaceId)
        {
            await mediator.Send(new CompleteDeliveryCommand(loadingPlaceId));
            return Ok();
        }

        [HttpPost("loadingPlace")]
        public async Task<IActionResult> AddLoadingPlace(AddLoadingPlaceCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }

        [HttpPut("loadingPlace")]
        public async Task<IActionResult> UpdateLoadingPlace(UpdateLoadingPlaceCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }

        [HttpDelete("loadingPlace/{loadingPlaceId}")]
        public async Task<IActionResult> DeleteLoadingPlace(int loadingPlaceId)
        {
            await mediator.Send(new DeleteLoadingPlaceCommand(loadingPlaceId));
            return Ok();
        }
    }
}