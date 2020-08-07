using BikeHttpClient;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ShopMVC.Extensions;
using ShopMVC.Interfaces;
using ShopMVC.Models;
using ShopMVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMVC.Services
{
    public class OrderService : IOrderService
    {
        private readonly string baseUrl;
        private readonly ICustomHttpClient customHttpClient;
        private readonly string token;
        private readonly string userId;

        public OrderService(ICustomHttpClient customHttpClient, IHttpContextAccessor httpContextAccessor)
        {
            baseUrl = "http://host.docker.internal:5100/api/CustomerOrder/";
            this.customHttpClient = customHttpClient;
            token = httpContextAccessor.HttpContext.GetToken();
            userId = httpContextAccessor.HttpContext.GetNameIdentifier();
        }

        public async Task CreateOrder(Order order)
        {
            order.UserId = userId;

            await customHttpClient.PostAsync(baseUrl, order, token);
        }

        public async Task<List<OrdersViewModel>> GetOrders()
        {
            var getOrdersUrl = $"{baseUrl}user/{userId}";

            var orders = await customHttpClient.GetStringAsync(getOrdersUrl, token);

            return JsonConvert.DeserializeObject<List<OrdersViewModel>>(orders);
        }

        public async Task<OrderDetailViewModel> GetOrderDetail(int id)
        {
            var getOrderDetailUrl = $"{baseUrl}{id}";

            var order = await customHttpClient.GetStringAsync(getOrderDetailUrl, token);

            return JsonConvert.DeserializeObject<OrderDetailViewModel>(order);
        }
    }
}
