namespace Common.Application.Messaging
{
    public class ProductionConfirmedResult
    {
        public bool StartProduction { get; set; }
        public string Reference { get; set; }
        public int Quantity { get; set; }
    }
}
