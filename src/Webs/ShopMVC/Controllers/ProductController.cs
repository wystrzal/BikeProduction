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

        public async Task<IActionResult> Index(int take, int skip)
        {
            var vm = new CatalogProductsViewModel
            {
                CatalogProducts = await catalogService.GetProducts(take, skip),
                Take = take,
                Skip = skip
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> GetProducts([FromBody]GetProductsDto getProductsDto)
        {
            var products = await catalogService.GetProducts(getProductsDto.Take, getProductsDto.Skip);

            if (products == null || products.Count < 0)
            {
                return Ok();
            }

            return Json(new { products });
        }
    }
}