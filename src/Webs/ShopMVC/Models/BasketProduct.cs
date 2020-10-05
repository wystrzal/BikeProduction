namespace ShopMVC.Models
{
    public class BasketProduct
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Reference { get; set; }
        public string PhotoUrl { get; set; }
        public int Quantity { get; set; }
    }
}
