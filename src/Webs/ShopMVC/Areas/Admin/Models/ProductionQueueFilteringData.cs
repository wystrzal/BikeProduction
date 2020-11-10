using static ShopMVC.Areas.Admin.Models.Enums.ProductionStatusEnum;

namespace Production.Core.Models
{
    public class ProductionQueueFilteringData
    {
        public ProductionStatus ProductionStatus { get; set; }
    }
}
