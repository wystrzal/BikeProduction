﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMVC.Models.ViewModels
{
    public class UserBasketViewModel
    {
        public List<BasketProduct> Products { get; set; } = new List<BasketProduct>();
        public decimal TotalPrice { get; set; }
        public string UserId { get; set; }
    }
}
