namespace Delivery.Core.Models.Enums
{
    public class PackStatusEnum
    {
        public enum PackStatus
        {
            Waiting = 1,
            ReadyToSend = 2,
            Sended = 3,
            Delivered = 4
        }
    }
}
