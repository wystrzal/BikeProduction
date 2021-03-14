using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Warehouse.Application.Commands;
using Warehouse.Application.Queries;

namespace Warehouse.API.Controllers
{
    [Authorize(Policy = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IMediator mediator;

        public WarehouseController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("part")]
        public async Task<IActionResult> AddPart(AddPartCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }

        [HttpDelete("part/{partId}")]
        public async Task<IActionResult> DeletePart(int partId)
        {
            await mediator.Send(new DeletePartCommand(partId));
            return Ok();
        }

        [HttpPut("part")]
        public async Task<IActionResult> UpdatePart(UpdatePartCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }

        [HttpGet("part/{id}")]
        public async Task<IActionResult> GetPart(int id)
        {
            return Ok(await mediator.Send(new GetPartQuery(id)));
        }

        [HttpGet("parts")]
        public async Task<IActionResult> GetParts()
        {
            return Ok(await mediator.Send(new GetPartsQuery()));
        }

        [HttpGet("product/{reference}/parts")]
        public async Task<IActionResult> GetProductParts(string reference)
        {
            return Ok(await mediator.Send(new GetProductPartsQuery(reference)));
        }

        [HttpDelete("product/{reference}/part/{partId}")]
        public async Task<IActionResult> DeleteProductPart(string reference, int partId)
        {
            await mediator.Send(new DeleteProductPartCommand(partId, reference));
            return Ok();
        }

        [HttpPost("product/{reference}/part/{partId}")]
        public async Task<IActionResult> AddProductPart(string reference, int partId)
        {
            await mediator.Send(new AddProductPartCommand(partId, reference));
            return Ok();
        }
    }
}