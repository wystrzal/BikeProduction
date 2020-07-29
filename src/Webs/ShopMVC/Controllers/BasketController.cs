using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopMVC.Interfaces;
using ShopMVC.Models;
using ShopMVC.Models.Dtos;
using static ShopMVC.Models.Enums.UpdateBasketEnum;

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
        public async Task<IActionResult> AddToBasket([FromBody]List<BasketProduct> basketProducts)
        {
            await basketService.UpdateBasket(basketProducts);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBasket([FromBody]UpdateBasketDto changeProductQuantity)
        {
            var basketProducts = await basketService.GetBasket() as List<BasketProduct>;

            var product = basketProducts.Where(x => x.Id == changeProductQuantity.ProductId).FirstOrDefault();

            if (changeProductQuantity.UpdateBasketAction == UpdateBasketAction.Plus)
            {
                product.Quantity++;
            } 
            else
            {
                product.Quantity--;
            }

            if (product.Quantity <= 0)
            {
                basketProducts.Remove(product);
            }

            await basketService.UpdateBasket(basketProducts);

            return Json(product.Quantity);
        }
    }
}