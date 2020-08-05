using Basket.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Core.Dtos
{
    public class AddProductDto
    {
        public BasketProduct Product { get; set; }
        public string UserId { get; set; }
    }
}
