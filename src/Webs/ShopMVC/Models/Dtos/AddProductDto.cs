using System;
using System.Collections.Generic;
using System.Text;

namespace ShopMVC.Models.Dtos
{
    public class AddProductDto
    {
        public BasketProduct Product { get; set; }
        public string UserId { get; set; }
    }
}
