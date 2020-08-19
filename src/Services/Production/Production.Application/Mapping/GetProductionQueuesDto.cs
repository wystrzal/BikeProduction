using static Production.Core.Models.Enums.ProductionStatusEnum;

namespace Production.Application.Mapping
{
    public class GetProductionQueuesDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Reference { get; set; }
        public int Quantity { get; set; }
        public ProductionStatus ProductionStatus { get; set; }
    }
}
