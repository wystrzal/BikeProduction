using Microsoft.AspNetCore.Mvc;
using ShopMVC.Filters;
using ShopMVC.Interfaces;
using ShopMVC.Models;
using ShopMVC.Models.ViewModels;
using System.Threading.Tasks;

namespace ShopMVC.Areas.Admin.Controllers
{
    [AdminAuthorization]
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public async Task<IActionResult> Index(OrderFilteringData filteringData)
        {
            filteringData ??= new OrderFilteringData();
            filteringData.UserIsAdmin = true;

            var vm = new OrdersViewModel
            {
                FilteringData = filteringData,
                PageSize = 50,
                Orders = await orderService.GetOrders(filteringData),
                CurrentPage = filteringData.Page
            };

            return View(vm);
        }
    }
}