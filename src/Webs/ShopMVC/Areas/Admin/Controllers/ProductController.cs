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

        public ProductController(ICatalogService catalogService)
        {
            this.catalogService = catalogService;
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
            var vm = new PostPutProductViewModel
            {
                Brand = await catalogService.GetBrandListItem(),
                Product = new CatalogProduct()
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CatalogProduct product)
        {
            var vm = await CreateVMIfAnyError(product);

            if (vm != null)
                return View(vm);

            await catalogService.AddProduct(product);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateProduct(int productId)
        {
            var vm = new PostPutProductViewModel
            {
                Brand = await catalogService.GetBrandListItem(),
                Product = await catalogService.GetProduct(productId)
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(CatalogProduct product)
        {
            var vm = await CreateVMIfAnyError(product);

            if (vm != null)
                return View(vm);

            await catalogService.UpdateProduct(product);

            return RedirectToAction("Index");
        }

        private async Task<PostPutProductViewModel> CreateVMIfAnyError(CatalogProduct product)
        {
            if (product.Price == 0)
            {
                ModelState.AddModelError("", "The Price field cannot be zero.");
            }

            if (ModelState.ErrorCount > 0)
            {
                var vm = new PostPutProductViewModel
                {
                    Brand = await catalogService.GetBrandListItem(),
                    Product = product.Id == 0 ? new CatalogProduct() : await catalogService.GetProduct(product.Id)
                };

                return vm;
            }

            return null;
        }

        public async Task<IActionResult> DeleteProduct(int productId)
        {
            await catalogService.DeleteProduct(productId);

            return RedirectToAction("Index");
        }
    }
}