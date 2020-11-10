using static ShopMVC.Models.Enums.OrderStatusEnum;

namespace ShopMVC.Models
{
    public class OrderFilteringData
    {
        public OrderStatus OrderStatus { get; set; }
        public bool UserIsAdmin { get; set; }
        public string UserId { get; set; }
        public int Page { get; set; } = 1;
    }
}
