using Microsoft.AspNetCore.Mvc;
using Production.Core.Models;
using ShopMVC.Areas.Admin.Models.ViewModels;
using ShopMVC.Filters;
using ShopMVC.Interfaces;
using System.Threading.Tasks;

namespace ShopMVC.Areas.Admin.Controllers
{
    [AdminAuthorization]
    [Area("Admin")]
    public class ProductionController : Controller
    {
        private readonly IProductionService productionService;
        public ProductionController(IProductionService productionService)
        {
            this.productionService = productionService;
        }

        public async Task<IActionResult> Index(ProductionQueueFilteringData filteringData)
        {
            var vm = new ProductionQueuesViewModel
            {
                ProductionQueues = await productionService.GetProductionQueues(filteringData ?? new ProductionQueueFilteringData())
            };

            return View(vm);
        }

        public async Task<IActionResult> ConfirmProduction(int productionId)
        {
            await productionService.ConfirmProduction(productionId);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> StartCreatingProducts(int productionId)
        {
            await productionService.StartCreatingProducts(productionId);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> FinishProduction(int productionId)
        {
            await productionService.FinishProduction(productionId);

            return RedirectToAction("Index");
        }
    }
}
