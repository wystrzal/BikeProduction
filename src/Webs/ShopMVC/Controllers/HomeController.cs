﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopMVC.Interfaces;
using ShopMVC.Models;
using ShopMVC.Models.ViewModels;
using static ShopMVC.Models.Enums.HomeProductEnum;

namespace ShopMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICatalogService catalogService;

        public HomeController(ICatalogService catalogService)
        {
            this.catalogService = catalogService;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new HomePageViewModel
            {
                NewProducts = await catalogService.GetHomeProducts(HomeProduct.NewProduct),
                PopularProducts = await catalogService.GetHomeProducts(HomeProduct.PopularProduct)
            };

            return View(vm);
        }
    }
}
