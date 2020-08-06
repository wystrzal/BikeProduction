using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopMVC.Interfaces;
using ShopMVC.Models.Dtos;
using ShopMVC.Models.ViewModels;

namespace ShopMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICatalogService catalogService;

        public ProductController(ICatalogService catalogService)
        {
            this.catalogService = catalogService;
        }

        public async Task<IActionResult> Index(CatalogProductsViewModel vm)
        {
            var skip = vm.CatalogProducts.Count;

            var catalogProducts = await catalogService.GetProducts(vm.Take, skip);

            vm.CatalogProducts.AddRange(catalogProducts);

            return View(vm);
        }
    }
}