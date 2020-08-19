using Microsoft.AspNetCore.Mvc;
using ShopMVC.Interfaces;
using ShopMVC.Models.ViewModels;
using System.Threading.Tasks;
using static ShopMVC.Models.Enums.HomeProductEnum;

namespace ShopMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICatalogService catalogService;

        public HomeController(ICatalogService catalogService)
        {
            this.catalogService = catalogService;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new HomePageViewModel
            {
                NewProducts = await catalogService.GetHomeProducts(HomeProduct.NewProduct),
                PopularProducts = await catalogService.GetHomeProducts(HomeProduct.PopularProduct)
            };

            return View(vm);
        }
    }
}
