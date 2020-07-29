using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Core.Models
{
    public class UserBasketDto
    {
        public List<BasketProduct> Products { get; set; } = new List<BasketProduct>();
        public decimal TotalPrice { get; set; }
        public string UserId { get; set; }
    }
}

