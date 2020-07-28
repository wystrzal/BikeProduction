using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopMVC.Interfaces;
using ShopMVC.Models;
using ShopMVC.Models.Dtos;

namespace ShopMVC.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService basketService;

        public BasketController(IBasketService basketService)
        {
            this.basketService = basketService;
        }

        public async Task<IActionResult> Index()
        {
            var basket = await basketService.GetBasket();

            basket ??= new List<BasketProduct>();

            return View(basket);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBasket([FromBody]List<BasketProduct> basketProducts)
        {
            await basketService.UpdateBasket(basketProducts);

            return Ok();
        }
    }
}