using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopMVC.Areas.Admin.Models.ViewModels;
using ShopMVC.Filters;
using ShopMVC.Interfaces;
using ShopMVC.Models;

namespace ShopMVC.Areas.Admin.Controllers
{
    [AdminAuthorization]
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ICatalogService catalogService;
        private readonly IWarehouseService warehouseService;

        public ProductController(ICatalogService catalogService, IWarehouseService warehouseService)
        {
            this.catalogService = catalogService;
            this.warehouseService = warehouseService;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new ProductsViewModel
            {
                Products = await catalogService.GetProducts(new CatalogFilteringData())
            };

            return View(vm);
        }

        public async Task<IActionResult> CreateProduct()
        {
            return await ReturnViewWithPostPutVM();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CatalogProduct product)
        {
            if (!ModelState.IsValid)
            {
                return await ReturnViewWithPostPutVM();
            }

            await catalogService.AddProduct(product);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateProduct(int productId)
        {
            return await ReturnViewWithPostPutVM(productId);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(CatalogProduct product)
        {
            if (!ModelState.IsValid)
            {
                return await ReturnViewWithPostPutVM(product.Id);
            }

            await catalogService.UpdateProduct(product);

            return RedirectToAction("Index");
        }

        private async Task<IActionResult> ReturnViewWithPostPutVM(int productId = 0)
        {
            var product = productId == 0 ? new CatalogProduct() : await catalogService.GetProduct(productId);

            var vm = new PostPutProductViewModel
            {
                Brand = await catalogService.GetBrandListItem(),
                Product = product,
                Parts = productId == 0 ? null : await warehouseService.GetProductParts(product.Reference)
            };

            return View(vm);
        }

        public async Task<IActionResult> DeleteProduct(int productId)
        {
            await catalogService.DeleteProduct(productId);

            return RedirectToAction("Index");
        }
    }
}