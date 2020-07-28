using System.Threading.Tasks;
using Basket.Core.Interfaces;
using Basket.Core.Models;
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
        public async Task<IActionResult> UpdateBasket(UserBasket userBasket)
        {
            await basketService.UpdateBasket(userBasket);
            return Ok();
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetBasket(string userId)
        {
            return Ok(await basketService.GetBasket(userId));
        }
    }
}