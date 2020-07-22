using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopMVC.Interfaces;

namespace ShopMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICatalogService catalogService;

        public ProductController(ICatalogService catalogService)
        {
            this.catalogService = catalogService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await catalogService.GetProducts());
        }
    }
}