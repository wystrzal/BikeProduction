﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopMVC.Areas.Admin.Models.ViewModels;
using ShopMVC.Filters;
using ShopMVC.Interfaces;

namespace ShopMVC.Areas.Admin.Controllers
{
    [AdminAuthorization]
    [Area("Admin")]
    public class ProductionController : Controller
    {
        private readonly IProductionService productionService;
        public ProductionController(IProductionService productionService)
        {
            this.productionService = productionService;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new ProductionQueuesViewModel
            {
                ProductionQueues = await productionService.GetProductionQueues()
            };

            return View(vm);
        }
    }
}
