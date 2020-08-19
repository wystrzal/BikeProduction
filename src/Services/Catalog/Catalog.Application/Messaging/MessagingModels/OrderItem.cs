namespace Catalog.Application.Messaging.MessagingModels
{
    public class OrderItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Reference { get; set; }
        public int Quantity { get; set; }
    }
}

