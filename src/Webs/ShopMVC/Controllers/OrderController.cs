using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopMVC.Filters;
using ShopMVC.Interfaces;
using ShopMVC.Models;
using ShopMVC.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMVC.Controllers
{
    [UserAuthorization]
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public async Task<IActionResult> Index(OrderFilteringData filteringData)
        {
            var vm = new OrdersViewModel
            {
                FilteringData = filteringData ??= new OrderFilteringData(),
                PageSize = 50,
                Orders = await orderService.GetOrders(filteringData),
                CurrentPage = filteringData.Page
            };

            return View(vm);
        }

        public async Task<IActionResult> OrderDetail(int orderId)
        {
            var order = await orderService.GetOrderDetail(orderId);

            return View(order);
        }

        public IActionResult CreateOrder(UserBasketViewModel vm)
        {
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody]Order order)
        {
            if (order.OrderItems == null || order.OrderItems.Count == 0 )
            {
                ModelState.AddModelError("", "You don't have any products in basket.");
            }

            if (ModelState.ErrorCount > 0 || !ModelState.IsValid)
            {
                return ReturnJsonWithErrors();
            }

            await orderService.CreateOrder(order);

            return Json(new { status = "success", url = Url.Action("Index", "Home") });
        }

        private IActionResult ReturnJsonWithErrors()
        {
            List<string> errors = new List<string>();

            foreach (var obj in ModelState.Values)
            {
                foreach (var error in obj.Errors)
                {
                    if (!string.IsNullOrEmpty(error.ErrorMessage))
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
            }

            return Json(new { status = "error", errors });
        }

        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            await orderService.DeleteOrder(orderId);

            return RedirectToAction("Index");
        }
    }
}