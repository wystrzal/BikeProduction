namespace Common.Application.Messaging
{
    public class OrderCanceledEvent
    {
        public int OrderId { get; set; }
        public OrderCanceledEvent(int orderId)
        {
            OrderId = orderId;
        }
    }
}
