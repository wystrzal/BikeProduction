using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Warehouse.Application.Commands;
using Warehouse.Application.Queries;

namespace Warehouse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ILogger<WarehouseController> logger;

        public WarehouseController(IMediator mediator, ILogger<WarehouseController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        [HttpPost("part")]
        public async Task<IActionResult> AddPart(AddPartCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }

        [HttpDelete("part")]
        public async Task<IActionResult> DeletePart(DeletePartCommand command)
        {
            try
            {
                await mediator.Send(new DeletePartCommand(command.PartId));
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("part/{id}")]
        public async Task<IActionResult> GetPart(int id)
        {
            try
            {
                return Ok(await mediator.Send(new GetPartQuery(id)));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("parts")]
        public async Task<IActionResult> GetParts()
        {
            return Ok(await mediator.Send(new GetPartsQuery()));
        }

        [HttpPost("storage-place")]
        public async Task<IActionResult> AddStoragePlace(AddPartCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }

        [HttpDelete("storage-place/{id}")]
        public async Task<IActionResult> DeleteStoragePlace(int id)
        {
            try
            {
                await mediator.Send(new DeleteStoragePlaceCommand(id));
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("storage-places")]
        public async Task<IActionResult> GetStoragePlaces()
        {
            return Ok(await mediator.Send(new GetStoragePlacesQuery()));
        }
    }
}