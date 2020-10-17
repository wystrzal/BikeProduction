namespace Delivery.Core.Models.Enums
{
    public class PackStatusEnum
    {
        public enum PackStatus
        {
            All = 0,
            Waiting = 1,
            ReadyToSend = 2,
            Sended = 3,
            Delivered = 4
        }
    }
}
