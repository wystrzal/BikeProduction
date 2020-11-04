using Basket.Core.Models;
using System.Collections.Generic;

namespace Basket.Core.Dtos
{
    public class UserBasketDto
    {
        public List<BasketProduct> Products { get; set; } = new List<BasketProduct>();
        public decimal TotalPrice { get; set; }
    }
}

