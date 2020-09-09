namespace Production.Application.Messaging.MessagingModels
{
    public class OrderStatusEnum
    {
        public enum OrderStatus
        {
            Waiting_For_Confirm = 1,
            Confirmed = 2,
            Ready_To_Send = 3,
            Sended = 4,
            Delivered = 5
        }
    }
}

