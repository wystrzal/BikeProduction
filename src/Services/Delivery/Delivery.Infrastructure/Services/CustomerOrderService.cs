using BikeHttpClient;
using Delivery.Core.Interfaces;
using Delivery.Core.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Infrastructure.Services
{
    public class CustomerOrderService : ICustomerOrderService
    {
        private readonly ICustomHttpClient httpClient;
        private readonly IHttpContextAccessor httpContext;
        private readonly string baseUrl;

        public CustomerOrderService(ICustomHttpClient httpClient, IHttpContextAccessor httpContext)
        {
            this.httpClient = httpClient;
            this.httpContext = httpContext;
            baseUrl = "http://localhost:5100/api/customerOrder/";
        }

        public async Task<IEnumerable<Order>> GetOrder(int id)
        {
            var getOrderUrl = baseUrl + id;

            var accessToken = httpContext.HttpContext.User.Claims.Where(x => x.Type == "AcessToken")
                .Select(x => x.Value).FirstOrDefault();

            var dataString = await httpClient.GetStringAsync(getOrderUrl, accessToken);

            var response = JsonConvert.DeserializeObject<IEnumerable<Order>>(dataString);

            return response;
        }
    }
}
