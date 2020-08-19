namespace Common.Application.Commands
{
    public class PackReadyToSendEvent
    {
        public int OrderId { get; set; }

        public PackReadyToSendEvent(int orderId)
        {
            OrderId = orderId;
        }
    }
}
