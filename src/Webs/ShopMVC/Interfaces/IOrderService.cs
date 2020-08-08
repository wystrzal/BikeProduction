using ShopMVC.Models;
using ShopMVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMVC.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrder(Order order);
        Task<List<OrdersViewModel>> GetOrders();
        Task<OrderDetailViewModel> GetOrderDetail(int id);
        Task DeleteOrder(int id);
    }
}
