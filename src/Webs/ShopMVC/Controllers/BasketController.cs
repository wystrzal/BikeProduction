using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopMVC.Interfaces;
using ShopMVC.Models;
using ShopMVC.Models.Dtos;
using ShopMVC.Models.ViewModels;
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

            basket ??= new UserBasketViewModel();

            basket.Products ??= new List<BasketProduct>();
        
            return View(basket);
        }

        [HttpPost]
        public async Task<IActionResult> AddToBasket([FromBody]List<BasketProduct> basketProducts)
        {
            await basketService.UpdateBasket(basketProducts);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBasket([FromBody]UpdateBasketDto updateBasketDto)
        {
            var basketProducts = await basketService.GetBasket();

            var product = basketProducts.Products.Where(x => x.Id == updateBasketDto.ProductId).FirstOrDefault();

            if (updateBasketDto.UpdateBasketAction == UpdateBasketAction.Plus)
            {
                product.Quantity++;
                basketProducts.TotalPrice += product.Price;
            } 
            else
            {
                product.Quantity--;
                basketProducts.TotalPrice -= product.Price;
            }

            if (product.Quantity <= 0)
            {
                basketProducts.Products.Remove(product);
            }

            await basketService.UpdateBasket(basketProducts.Products);

            return Json(new { product.Quantity, basketProducts.TotalPrice});
        }
    }
}