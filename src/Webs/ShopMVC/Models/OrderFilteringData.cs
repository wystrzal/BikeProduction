using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ShopMVC.Models.Enums.OrderStatusEnum;

namespace ShopMVC.Models
{
    public class OrderFilteringData
    {
        public int Take { get; set; }
        public int Skip { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string UserId { get; set; }
    }
}
