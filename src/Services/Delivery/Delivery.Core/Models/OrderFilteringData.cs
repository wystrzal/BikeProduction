using static Delivery.Core.Models.Enums.PackStatusEnum;

namespace Delivery.Core.SearchSpecification
{
    public class OrderFilteringData
    {
        public PackStatus PackStatus { get; set; }
    }
}
