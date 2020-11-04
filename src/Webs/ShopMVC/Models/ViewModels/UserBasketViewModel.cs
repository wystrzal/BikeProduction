using System.Collections.Generic;

namespace ShopMVC.Models.ViewModels
{
    public class UserBasketViewModel
    {
        public List<BasketProduct> Products { get; set; } = new List<BasketProduct>();
        public decimal TotalPrice { get; set; }
    }
}

