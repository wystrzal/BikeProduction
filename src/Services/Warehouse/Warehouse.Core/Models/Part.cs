using System.Collections.Generic;

namespace Warehouse.Core.Models
{
    public class Part
    {
        public int Id { get; set; }
        public string PartName { get; set; }
        public string Reference { get; set; }
        public int QuantityForProduction { get; set; }
        public int Quantity { get; set; } = 0;
        public virtual ICollection<ProductsParts> ProductsParts { get; set; }
        public StoragePlace StoragePlace { get; set; }
    }
}
