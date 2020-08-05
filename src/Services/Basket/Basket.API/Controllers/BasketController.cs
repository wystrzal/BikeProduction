using System.Threading.Tasks;
using Basket.Core.Dtos;
using Basket.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService basketService;

        public BasketController(IBasketService basketService)
        {
            this.basketService = basketService;
        }

        [HttpPost("change/quantity")]
        public async Task<IActionResult> ChangeProductQuantity(ChangeProductQuantityDto changeProductQuantityDto)
        {
            await basketService.ChangeProductQuantity(changeProductQuantityDto);

            return Ok();
        }

        [HttpPost("add/product")]
        public async Task<IActionResult> AddProduct(AddProductDto addProductDto)
        {
            await basketService.AddProduct(addProductDto);

            return Ok();
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetBasket(string userId)
        {
            return Ok(await basketService.GetBasket(userId));
        }

        [HttpGet("{userId}/quantity")]
        public async Task<IActionResult> GetBasketQuantity(string userId)
        {
            return Ok(await basketService.GetBasketQuantity(userId));
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> ClearBasket(string userId)
        {
            await basketService.RemoveBasket(userId);

            return Ok();
        }

        [HttpDelete("{userId}/product/{productId}")]
        public async Task<IActionResult> DeleteProduct(string userId, int productId)
        {
            await basketService.RemoveProduct(userId, productId);

            return Ok();
        }
    }
}