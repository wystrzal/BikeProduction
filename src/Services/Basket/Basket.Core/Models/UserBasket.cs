using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Core.Models
{
    public class UserBasket
    {
        public List<Product> Products { get; set; } = new List<Product>();
        public string UserId { get; set; }
    }
}
