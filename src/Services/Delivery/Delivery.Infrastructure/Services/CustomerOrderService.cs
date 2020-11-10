using BikeHttpClient;
using Delivery.Core.Interfaces;
using Delivery.Core.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Delivery.Infrastructure.Services
{
    public class CustomerOrderService : ICustomerOrderService
    {
        private readonly ICustomHttpClient httpClient;
        private readonly string baseUrl;

        public CustomerOrderService(ICustomHttpClient httpClient)
        {
            this.httpClient = httpClient;
            baseUrl = "http://host.docker.internal:5100/api/CustomerOrder/";
        }

        public async Task<Order> GetOrder(int id, string token)
        {
            var getOrderUrl = $"{baseUrl}{id}";

            var dataString = await httpClient.GetStringAsync(getOrderUrl, token);

            var response = JsonConvert.DeserializeObject<Order>(dataString);

            return response;
        }
    }
}
