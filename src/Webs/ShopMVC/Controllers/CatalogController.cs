using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopMVC.Interfaces;
using ShopMVC.Models;
using ShopMVC.Models.Dtos;
using ShopMVC.Models.ViewModels;

namespace ShopMVC.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ICatalogService catalogService;

        public CatalogController(ICatalogService catalogService)
        {
            this.catalogService = catalogService;
        }

        public async Task<IActionResult> Index(CatalogProductsViewModel vm)
        {
            vm.FilteringData ??= new FilteringData();

            vm.BrandListItem = await catalogService.GetBrandListItem();

            vm.FilteringData.Take = vm.FilteringData.Take == 0 ? 6 : vm.FilteringData.Take;

            vm.FilteringData.Skip = vm.CatalogProducts.Count;

            var catalogProducts = await catalogService.GetProducts(vm.FilteringData);

            vm.CatalogProducts.AddRange(catalogProducts);

            return View(vm);
        }

        public async Task<IActionResult> ProductDetail(int id)
        {
            var product = await catalogService.GetProduct(id);

            return View(product);
        }
    }
}