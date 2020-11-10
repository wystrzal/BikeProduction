using Microsoft.AspNetCore.Mvc;
using ShopMVC.Areas.Admin.Models;
using ShopMVC.Areas.Admin.Models.Dto;
using ShopMVC.Areas.Admin.Models.ViewModels;
using ShopMVC.Filters;
using ShopMVC.Interfaces;
using System.Threading.Tasks;

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

        public async Task<IActionResult> Index(string reference, int productId)
        {
            var vm = new PartsViewModel
            {
                Parts = await warehouseService.GetParts(),
                Reference = reference,
                ProductId = productId
            };

            return View(vm);
        }

        public async Task<IActionResult> AddProductPart(AddProductPartDto dto)
        {
            await warehouseService.AddProductPart(dto.Reference, dto.PartId);

            return RedirectToAction("UpdateProduct", "Product", new { productId = dto.ProductId });
        }

        public async Task<IActionResult> DeletePart(int partId)
        {
            await warehouseService.DeletePart(partId);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdatePart(int partId)
        {
            return await ReturnViewWithPostPutVM(partId);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePart(Part part)
        {
            if (!ModelState.IsValid)
            {
                return await ReturnViewWithPostPutVM(part.Id);
            }

            await warehouseService.UpdatePart(part);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> CreatePart()
        {
            return await ReturnViewWithPostPutVM();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePart(Part part)
        {
            if (!ModelState.IsValid)
            {
                return await ReturnViewWithPostPutVM();
            }

            await warehouseService.AddPart(part);

            return RedirectToAction("Index");
        }

        private async Task<IActionResult> ReturnViewWithPostPutVM(int partId = 0)
        {
            var vm = new PostPutPartViewModel
            {
                Part = partId == 0 ? null : await warehouseService.GetPart(partId)
            };

            return View(vm);
        }
    }
}
