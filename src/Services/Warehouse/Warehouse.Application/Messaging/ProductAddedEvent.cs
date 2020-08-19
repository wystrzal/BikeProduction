namespace Common.Application.Messaging
{
    public class ProductAddedEvent
    {
        public string ProductName { get; set; }
        public string Reference { get; set; }

        public ProductAddedEvent(string productName, string reference)
        {
            ProductName = productName;
            Reference = reference;
        }
    }
}
