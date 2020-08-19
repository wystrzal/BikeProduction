using System;
using static ShopMVC.Models.Enums.OrderStatusEnum;

namespace ShopMVC.Models.ViewModels
{
    public class OrdersViewModel
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
