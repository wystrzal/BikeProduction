namespace ShopMVC.Areas.Admin.Models.Enums
{
    public class ProductionStatusEnum
    {
        public enum ProductionStatus
        {
            Waiting = 1,
            NoParts = 2,
            Confirmed = 3,
            BeingCreated = 4,
            Finished = 5
        }
    }
}
