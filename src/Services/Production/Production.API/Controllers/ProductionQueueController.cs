using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Production.Application.Commands;

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
                await mediator.Send(new ConfirmProductionCommand { ProductionQueueId = id });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}