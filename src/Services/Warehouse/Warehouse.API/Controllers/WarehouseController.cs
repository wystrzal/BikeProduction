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

        [HttpDelete("part/{partId}")]
        public async Task<IActionResult> DeletePart(int partId)
        {
            try
            {
                await mediator.Send(new DeletePartCommand(partId));
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("part")]
        public async Task<IActionResult> UpdatePart(UpdatePartCommand command)
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

        [HttpGet("product/{reference}/parts")]
        public async Task<IActionResult> GetProductParts(string reference)
        {
            try
            {
                return Ok(await mediator.Send(new GetProductPartsQuery(reference)));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}