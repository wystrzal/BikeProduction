﻿using Basket.Application.Commands;
using Basket.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IMediator mediator;

        public BasketController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("change/quantity")]
        public async Task<IActionResult> ChangeProductQuantity(ChangeProductQuantityCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }

        [HttpPost("add/product")]
        public async Task<IActionResult> AddProduct(AddProductCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetBasket(string userId)
        {
            return Ok(await mediator.Send(new GetBasketQuery(userId)));
        }

        [HttpGet("{userId}/quantity")]
        public async Task<IActionResult> GetBasketQuantity(string userId)
        {
            return Ok(await mediator.Send(new GetBasketQuantityQuery(userId)));
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> ClearBasket(string userId)
        {
            await mediator.Send(new ClearBasketCommand(userId));
            return Ok();
        }

        [HttpDelete("{userId}/product/{productId}")]
        public async Task<IActionResult> DeleteProduct(string userId, int productId)
        {
            await mediator.Send(new RemoveProductCommand(userId, productId));
            return Ok();
        }
    }
}