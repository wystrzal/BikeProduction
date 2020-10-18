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
    public class DeliveryController : Controller
    {
        private readonly IDeliveryService deliveryService;

        public DeliveryController(IDeliveryService deliveryService)
        {
            this.deliveryService = deliveryService;
        }

        public async Task<IActionResult> Index(PackFilteringData filteringData)
        {
            var vm = new DeliveryViewModel
            {
                Packs = await deliveryService.GetPacks(filteringData ?? new PackFilteringData())
            };

            return View(vm);
        }

        public async Task<IActionResult> PackDetail(int id)
        {
            var vm = new PackDetailViewModel
            {
                PackToDelivery = await deliveryService.GetPack(id)
            };

            return View(vm);
        }
    }
}
