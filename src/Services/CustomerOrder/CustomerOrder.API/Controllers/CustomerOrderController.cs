﻿using CustomerOrder.Application.Commands;
using CustomerOrder.Application.Queries;
using CustomerOrder.Core.SearchSpecification;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

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
            await mediator.Send(command);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            return Ok(await mediator.Send(new GetOrderQuery(id)));
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders([FromQuery] FilteringData filteringData)
        {
            return Ok(await mediator.Send(new GetOrdersQuery(filteringData)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await mediator.Send(new DeleteOrderCommand(id));
            return Ok();
        }
    }
}