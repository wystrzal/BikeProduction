using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static ShopMVC.Models.Enums.OrderStatusEnum;

namespace ShopMVC.Models
{

    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus OrderStatus { get; set; }

        public string UserId { get; set; }

        [Required]
        public string CustomerFirstName { get; set; }

        [Required]
        public string CustomerLastName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string PostCode { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string HouseNumber { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        public List<BasketProduct> OrderItems { get; set; }
    }
}
