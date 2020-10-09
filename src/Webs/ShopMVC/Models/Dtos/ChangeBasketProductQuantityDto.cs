using static ShopMVC.Models.Enums.ChangeProductQuantityEnum;

namespace ShopMVC.Models.Dtos
{
    public class ChangeBasketProductQuantityDto
    {
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public ChangeQuantityAction ChangeQuantityAction { get; set; }
    }
}
