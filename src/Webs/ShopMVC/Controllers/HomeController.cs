using Microsoft.AspNetCore.Mvc;
using ShopMVC.Interfaces;
using ShopMVC.Models;
using ShopMVC.Models.ViewModels;
using System.Diagnostics;
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
