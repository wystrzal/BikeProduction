﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Core.Models
{
    public class UserBasket
    {
        public List<BasketProduct> Products { get; set; } = new List<BasketProduct>();
        public string UserId { get; set; }
    }
}
