using System;
using System.Collections.Generic;
using static ShopMVC.Models.Enums.OrderStatusEnum;

namespace ShopMVC.Models.ViewModels
{
    public class OrderDetailViewModel
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public List<BasketProduct> OrderItems { get; set; }
    }
}
