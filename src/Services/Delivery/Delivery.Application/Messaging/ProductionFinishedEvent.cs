namespace Common.Application.Messaging
{
    public class ProductionFinishedEvent
    {
        public int OrderId { get; set; }
        public int ProductsQuantity { get; set; }
        public string Token { get; set; }

        public ProductionFinishedEvent(int orderId, int productsQuantity, string token)
        {
            OrderId = orderId;
            ProductsQuantity = productsQuantity;
            Token = token;
        }
    }
}
