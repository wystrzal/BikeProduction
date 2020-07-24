using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basket.Core.Interfaces;
using Basket.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IDistributedCache distributedCache;
        private readonly IBasketService basketService;

        public BasketController(IDistributedCache distributedCache, IBasketService basketService)
        {
            this.distributedCache = distributedCache;
            this.basketService = basketService;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBasket(UserBasket basket)
        {
            await basketService.UpdateBasket(basket);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket(string userId)
        {
            return Ok(await basketService.GetBasket(userId));
        }
    }
}