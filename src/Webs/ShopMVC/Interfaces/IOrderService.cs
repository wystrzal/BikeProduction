using ShopMVC.Models;
using ShopMVC.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopMVC.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrder(Order order);
        Task<List<Order>> GetOrders(OrderFilteringData filteringData);
        Task<OrderDetailViewModel> GetOrderDetail(int id);
        Task DeleteOrder(int id);
    }
}
