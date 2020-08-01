using BikeHttpClient;
using Microsoft.AspNetCore.Http;
using ShopMVC.Extensions;
using ShopMVC.Interfaces;
using ShopMVC.Models;
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
        private readonly IHttpContextAccessor httpContextAccessor;

        public OrderService(ICustomHttpClient customHttpClient, IHttpContextAccessor httpContextAccessor)
        {
            baseUrl = "http://host.docker.internal:5100/api/CustomerOrder/";
            this.customHttpClient = customHttpClient;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task CreateOrder(Order order)
        {
            order.UserId = httpContextAccessor.HttpContext.GetNameIdentifier();

            var token = httpContextAccessor.HttpContext.GetToken();

            await customHttpClient.PostAsync(baseUrl, order, token);
        }
    }
}
