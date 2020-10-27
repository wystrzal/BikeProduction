using System.Collections.Generic;

namespace Warehouse.Core.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Reference { get; set; }
        public virtual ICollection<ProductsParts> ProductsParts { get; set; }
    }
}
