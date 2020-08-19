namespace Warehouse.Core.Models
{
    public class ProductsParts
    {
        public int PartId { get; set; }
        public Part Part { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
