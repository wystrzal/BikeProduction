using Microsoft.AspNetCore.Mvc;
using ShopMVC.Areas.Admin.Models;
using ShopMVC.Areas.Admin.Models.ViewModels;
using ShopMVC.Filters;
using ShopMVC.Interfaces;
using System.Threading.Tasks;

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

        public async Task<IActionResult> PackDetail(int packId)
        {
            var vm = new PackDetailViewModel
            {
                PackToDelivery = await deliveryService.GetPack(packId),
                LoadingPlaces = await deliveryService.GetLoadingPlaces(new LoadingPlaceFilteringData())
            };

            return View(vm);
        }

        public async Task<IActionResult> LoadingPlaceDetail(int loadingPlaceId)
        {
            var vm = new LoadingPlaceDetailViewModel
            {
                LoadingPlace = await deliveryService.GetLoadingPlace(loadingPlaceId)
            };

            return View(vm);
        }

        public async Task<IActionResult> DeleteLoadingPlace(int loadingPlaceId)
        {
            await deliveryService.DeleteLoadingPlace(loadingPlaceId);

            return RedirectToAction("IndexLoadingPlace");
        }

        public async Task<IActionResult> CreateLoadingPlace()
        {
            return await ReturnViewWithPostPutVM();
        }

        [HttpPost]
        public async Task<IActionResult> CreateLoadingPlace(LoadingPlace loadingPlace)
        {
            if (!ModelState.IsValid)
            {
                return await ReturnViewWithPostPutVM();
            }

            await deliveryService.AddLoadingPlace(loadingPlace);

            return RedirectToAction("IndexLoadingPlace");
        }

        public async Task<IActionResult> UpdateLoadingPlace(int loadingPlaceId)
        {
            return await ReturnViewWithPostPutVM(loadingPlaceId);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateLoadingPlace(LoadingPlace loadingPlace)
        {
            if (loadingPlace.AmountOfSpace < loadingPlace.LoadedQuantity)
            {
                ModelState.AddModelError("", "AmountOfSpace must be greater than LoadedQuantity");
            }

            if (!ModelState.IsValid)
            {
                return await ReturnViewWithPostPutVM(loadingPlace.Id);
            }

            await deliveryService.UpdateLoadingPlace(loadingPlace);

            return RedirectToAction("IndexLoadingPlace");
        }

        private async Task<IActionResult> ReturnViewWithPostPutVM(int loadingPlaceId = 0)
        {
            var vm = new PostPutLoadingPlaceViewModel
            {
                LoadingPlace = loadingPlaceId == 0 ? new LoadingPlace() : await deliveryService.GetLoadingPlace(loadingPlaceId)
            };

            return View(vm);
        }

        public async Task<IActionResult> LoadPack(int loadingPlaceId, int packId)
        {
            await deliveryService.LoadPack(loadingPlaceId, packId);

            return RedirectToAction("PackDetail", new { packId });
        }

        public async Task<IActionResult> StartDelivery(int loadingPlaceId)
        {
            await deliveryService.StartDelivery(loadingPlaceId);

            return RedirectToAction("LoadingPlaceDetail", new { loadingPlaceId });
        }

        public async Task<IActionResult> CompleteDelivery(int loadingPlaceId)
        {
            await deliveryService.CompleteDelivery(loadingPlaceId);

            return RedirectToAction("LoadingPlaceDetail", new { loadingPlaceId });
        }
    }
}