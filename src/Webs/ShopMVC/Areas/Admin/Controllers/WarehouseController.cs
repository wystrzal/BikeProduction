﻿using System;
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

        public async Task<IActionResult> UpdatePart(int partId)
        {
            var vm = new PostPutPartViewModel
            {
                Part = await warehouseService.GetPart(partId)
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePart(Part part)
        {
            if (!ModelState.IsValid)
            {
                return ReturnViewWithPostPutVM();
            }

            await warehouseService.UpdatePart(part);

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
            if (!ModelState.IsValid)
            {
                return ReturnViewWithPostPutVM();
            }

            await warehouseService.AddPart(part);

            return RedirectToAction("Index");
        }

        private IActionResult ReturnViewWithPostPutVM()
        {
            var vm = new PostPutPartViewModel
            {
                Part = new Part()
            };

            return View(vm);
        }
    }
}
