﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerOrder.Application.Commands;
using CustomerOrder.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerOrder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerOrderController : ControllerBase
    {
        private readonly IMediator mediator;

        public CustomerOrderController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderCommand command)
        {
            try
            {
                await mediator.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            try
            {
                return Ok(await mediator.Send(new GetOrderQuery(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders([FromQuery] string userId)
        {
            return Ok(await mediator.Send(new GetOrdersQuery(userId)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            try
            {
                await mediator.Send(new CancelOrderCommand(id));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}