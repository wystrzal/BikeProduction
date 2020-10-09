using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopMVC.Filters;
using ShopMVC.Interfaces;
using ShopMVC.Models;
using ShopMVC.Models.Dtos;
using ShopMVC.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopMVC.Controllers
{
    [UserAuthorization]
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
        public async Task<IActionResult> AddProduct([FromBody]BasketProduct basketProduct)
        {
            await basketService.AddProduct(basketProduct);

            var basketQuantity = await basketService.GetBasketQuantity();

            return Json(new { basketQuantity });
        }

        public async Task<IActionResult> ClearBasket()
        {
            await basketService.ClearBasket();

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> RemoveProduct(int productId)
        {
            await basketService.RemoveProduct(productId);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ChangeProductQuantity(ChangeBasketProductQuantityDto changeProductQuantityDto)
        {
            await basketService.ChangeProductQuantity(changeProductQuantityDto);

            return RedirectToAction("Index");
        }
    }
}