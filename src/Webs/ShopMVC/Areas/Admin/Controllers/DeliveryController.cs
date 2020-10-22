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

        public async Task<IActionResult> IndexPack(PackFilteringData filteringData)
        {
            var vm = new PacksViewModel
            {
                Packs = await deliveryService.GetPacks(filteringData ?? new PackFilteringData())
            };

            return View(vm);
        }

        public async Task<IActionResult> IndexLoadingPlace(LoadingPlaceFilteringData filteringData)
        {
            var vm = new LoadingPlacesViewModel
            {
                LoadingPlaces = await deliveryService.GetLoadingPlaces(filteringData ?? new LoadingPlaceFilteringData())
            };

            return View(vm);
        }

        public async Task<IActionResult> PackDetail(int id)
        {
            var vm = new PackDetailViewModel
            {
                PackToDelivery = await deliveryService.GetPack(id),
                LoadingPlaces = await deliveryService.GetLoadingPlaces(new LoadingPlaceFilteringData())
            };

            return View(vm);
        }

        public async Task<IActionResult> LoadingPlaceDetail(int id)
        {
            var vm = new LoadingPlaceDetailViewModel
            {
                LoadingPlace = await deliveryService.GetLoadingPlace(id)
            };

            return View(vm);
        }

        public async Task<IActionResult> DeleteLoadingPlace(int id)
        {
            await deliveryService.DeleteLoadingPlace(id);

            return RedirectToAction("IndexLoadingPlace");
        }

        public IActionResult CreateLoadingPlace()
        {
            var vm = new PostPutLoadingPlaceViewModel
            {
                LoadingPlace = new LoadingPlace()
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLoadingPlace(LoadingPlace loadingPlace)
        {
            var vm = CreatePostPutLoadingPlaceViewModelIfAnyError(loadingPlace);

            if (vm != null)
            {
                return View(vm);
            }

            await deliveryService.AddLoadingPlace(loadingPlace);

            return RedirectToAction("IndexLoadingPlace");
        }

        public async Task<IActionResult> UpdateLoadingPlace(int loadingPlaceId)
        {
            var vm = new PostPutLoadingPlaceViewModel
            {
                LoadingPlace = await deliveryService.GetLoadingPlace(loadingPlaceId)
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateLoadingPlace(LoadingPlace loadingPlace)
        {
            var vm = CreatePostPutLoadingPlaceViewModelIfAnyError(loadingPlace);

            if (vm != null)
            {
                return View(vm);
            }

            await deliveryService.UpdateLoadingPlace(loadingPlace);

            return RedirectToAction("IndexLoadingPlace");
        }

        private PostPutLoadingPlaceViewModel CreatePostPutLoadingPlaceViewModelIfAnyError(LoadingPlace loadingPlace)
        {
            if (loadingPlace.AmountOfSpace == 0)
            {
                ModelState.AddModelError("", "The Amount of space field cannot be zero.");
            }

            if (loadingPlace.AmountOfSpace < loadingPlace.LoadedQuantity)
            {
                ModelState.AddModelError("", "Amount of space must be greater than loaded quantity.");
            }

            if (ModelState.ErrorCount > 0)
            {
                var vm = new PostPutLoadingPlaceViewModel
                {
                    LoadingPlace = new LoadingPlace()
                };

                return vm;
            }

            return null;
        }

        public async Task<IActionResult> LoadPack(int loadingPlaceId, int packId)
        {
            await deliveryService.LoadPack(loadingPlaceId, packId);

            return RedirectToAction("PackDetail", new { id = packId });
        }

        public async Task<IActionResult> StartDelivery(int loadingPlaceId)
        {
            await deliveryService.StartDelivery(loadingPlaceId);

            return RedirectToAction("LoadingPlaceDetail", new { id = loadingPlaceId });
        }

        public async Task<IActionResult> CompleteDelivery(int loadingPlaceId)
        {
            await deliveryService.CompleteDelivery(loadingPlaceId);

            return RedirectToAction("LoadingPlaceDetail", new { id = loadingPlaceId });
        }
    }
}
