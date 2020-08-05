using System;
using System.Collections.Generic;
using System.Text;
using static ShopMVC.Models.Enums.ChangeProductQuantityEnum;

namespace ShopMVC.Models.Dtos
{
    public class ChangeProductQuantityDto
    {
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public ChangeQuantityAction ChangeQuantityAction { get; set; }
    }
}
