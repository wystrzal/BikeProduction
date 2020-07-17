﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Application.Commands;
using Warehouse.Application.Queries;

namespace Warehouse.API.Controllers
{
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
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("parts")]
        public async Task<IActionResult> GetParts()
        {
            return Ok(await mediator.Send(new GetPartsQuery()));
        }
    }
}