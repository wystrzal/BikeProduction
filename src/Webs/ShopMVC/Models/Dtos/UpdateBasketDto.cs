using ShopMVC.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ShopMVC.Models.Enums.UpdateBasketEnum;

namespace ShopMVC.Models.Dtos
{
    public class UpdateBasketDto
    {
        public UpdateBasketAction UpdateBasketAction { get; set; }
        public int ProductId { get; set; }
    }
}

