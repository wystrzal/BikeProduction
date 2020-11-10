using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopMVC.Models.ViewModels
{
    public class OrdersViewModel
    {
        public List<Order> Orders { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount()
        {
            return Convert.ToInt32(Math.Ceiling(Orders.Count / (double)PageSize));
        }
        public List<Order> PaginatedOrders()
        {
            int start = (CurrentPage - 1) * PageSize;
            return Orders.Skip(start).Take(PageSize).ToList();
        }

        public OrderFilteringData FilteringData { get; set; }
    }
}
