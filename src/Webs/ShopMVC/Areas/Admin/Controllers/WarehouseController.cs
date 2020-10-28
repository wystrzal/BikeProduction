using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopMVC.Areas.Admin.Models;
using ShopMVC.Areas.Admin.Models.ViewModels;
using ShopMVC.Filters;
using ShopMVC.Interfaces;

namespace ShopMVC.Areas.Admin.Controllers
{
    [AdminAuthorization]
    [Area("Admin")]
    public class WarehouseController : Controller
    {
        private readonly IWarehouseService warehouseService;

        public WarehouseController(IWarehouseService warehouseService)
        {
            this.warehouseService = warehouseService;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new PartsViewModel
            {
                Parts = await warehouseService.GetParts()
            };

            return View(vm);
        }

        public async Task<IActionResult> DeletePart(int partId)
        {
            await warehouseService.DeletePart(partId);

            return RedirectToAction("Index");
        }

        public IActionResult CreatePart()
        {
            var vm = new PostPutPartViewModel
            {
                Part = new Part()
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePart(Part part)
        {
            var vm = await CreatePostPutPartViewModelIfAnyError(part);

            if (vm != null)
            {
                return View(vm);
            }

            await warehouseService.AddPart(part);

            return RedirectToAction("Index");
        }

        private async Task<PostPutPartViewModel> CreatePostPutPartViewModelIfAnyError(Part part)
        {
            if (ModelState.ErrorCount > 0)
            {
                var vm = new PostPutPartViewModel
                {
                    Part = part.Id == 0 ? new Part() : await warehouseService.GetPart(part.Id)
                };

                return vm;
            }

            return null;
        }
    }
}
