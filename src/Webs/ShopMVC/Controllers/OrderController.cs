using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopMVC.Filters;
using ShopMVC.Interfaces;
using ShopMVC.Models;
using ShopMVC.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopMVC.Controllers
{
    [AuthorizeActionFilter]
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

            var vm = new OrdersViewModel
            {
                PageSize = 25,
                Orders = await orderService.GetOrders(filteringData),
                CurrentPage = filteringData.Page
            };

            return View(vm);
        }

        public async Task<IActionResult> OrderDetail(int id)
        {
            var order = await orderService.GetOrderDetail(id);

            return View(order);
        }

        public IActionResult CreateOrder(UserBasketViewModel vm)
        {
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody]Order order)
        {
            if (order.OrderItems.Count == 0 || order.OrderItems == null)
            {
                ModelState.AddModelError("", "You don't have any products in basket.");
            }

            if (ModelState.ErrorCount > 0 || !ModelState.IsValid)
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

            await orderService.CreateOrder(order);

            return Json(new { status = "success", url = Url.Action("Index", "Home") });
        }

        public async Task<IActionResult> DeleteOrder(int id)
        {
            await orderService.DeleteOrder(id);

            return RedirectToAction("Index");
        }
    }
}