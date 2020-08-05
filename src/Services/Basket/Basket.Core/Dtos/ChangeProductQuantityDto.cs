using System;
using System.Collections.Generic;
using System.Text;
using static Basket.Core.Dtos.Enums.ChangeProductQuantityEnum;

namespace Basket.Core.Dtos
{
    public class ChangeProductQuantityDto
    {
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public ChangeQuantityAction ChangeQuantityAction { get; set; }
    }
}
