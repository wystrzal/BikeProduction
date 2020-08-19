using Basket.Application.Commands;
using Basket.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
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

        [HttpPost("add/product")]
        public async Task<IActionResult> AddProduct(AddProductCommand command)
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

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetBasket(string userId)
        {
            try
            {
                return Ok(await mediator.Send(new GetBasketQuery(userId)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{userId}/quantity")]
        public async Task<IActionResult> GetBasketQuantity(string userId)
        {
            try
            {
                return Ok(await mediator.Send(new GetBasketQuantityQuery(userId)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> ClearBasket(string userId)
        {
            try
            {
                await mediator.Send(new ClearBasketCommand(userId));

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{userId}/product/{productId}")]
        public async Task<IActionResult> DeleteProduct(string userId, int productId)
        {
            try
            {
                await mediator.Send(new RemoveProductCommand(userId, productId));

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}