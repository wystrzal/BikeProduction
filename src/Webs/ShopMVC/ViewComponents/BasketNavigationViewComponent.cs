using Microsoft.AspNetCore.Mvc;
using ShopMVC.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMVC.ViewComponents
{
    public class BasketNavigationViewComponent : ViewComponent
    {
        private readonly IBasketService basketService;

        public BasketNavigationViewComponent(IBasketService basketService)
        {
            this.basketService = basketService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var basketQuantity = await basketService.GetBasketQuantity();

            return View("BasketNavigation", basketQuantity);
        }
    }
}
