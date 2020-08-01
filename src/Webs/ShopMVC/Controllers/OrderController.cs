using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopMVC.Filters;
using ShopMVC.Interfaces;
using ShopMVC.Models;
using ShopMVC.Models.ViewModels;

namespace ShopMVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
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
            return RedirectToAction("Index", "Home");
        }
    }
}