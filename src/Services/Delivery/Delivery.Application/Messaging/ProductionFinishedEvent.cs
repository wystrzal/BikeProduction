namespace Common.Application.Messaging
{
    public class ProductionFinishedEvent
    {
        public int OrderId { get; set; }
        public int ProductsQuantity { get; set; }

        public ProductionFinishedEvent(int orderId, int productsQuantity)
        {
            OrderId = orderId;
            ProductsQuantity = productsQuantity;
        }
    }
}
