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

        [HttpPost]
        public async Task<IActionResult> UpdateBasket(UserBasketDto userBasket)
        {
            await basketService.UpdateBasket(userBasket);
            return Ok();
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetBasket(string userId)
        {
            return Ok(await basketService.GetBasket(userId));
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