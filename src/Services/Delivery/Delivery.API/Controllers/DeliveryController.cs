using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Delivery.Application.Commands;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly IMediator mediator;

        public DeliveryController(IMediator mediator)
        {
            this.mediator = mediator;
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
                return BadRequest(ex.Message);
            }
        }

    }
}