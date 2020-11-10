namespace Common.Application.Messaging
{
    public class ProductUpdatedEvent
    {
        public string ProductName { get; set; }
        public string Reference { get; set; }
        public string OldReference { get; set; }

        public ProductUpdatedEvent(string productName, string reference, string oldReference)
        {
            ProductName = productName;
            Reference = reference;
            OldReference = oldReference;
        }
    }
}
