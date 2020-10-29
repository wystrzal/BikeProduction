using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Warehouse.Core.Models
{
    public class Part
    {
        public int Id { get; set; }
        public string PartName { get; set; }
        public string Reference { get; set; }
        public int QuantityForProduction { get; set; }
        public int Quantity { get; set; }
        public virtual ICollection<ProductsParts> ProductsParts { get; set; }
    }
}
