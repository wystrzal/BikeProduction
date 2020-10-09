namespace ShopMVC.Models.Dtos
{
    public class AddProductToBasketDto
    {
        public BasketProduct Product { get; set; }
        public string UserId { get; set; }
    }
}
