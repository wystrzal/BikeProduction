using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Core.Models
{
    public class Part
    {
        public int Id { get; set; }
        public string PartName { get; set; }
        public string Reference { get; set; }
        public int Quantity { get; set; } = 0;
        public ICollection<ProductsParts> ProductsParts { get; set; }
        public int StoragePlaceId { get; set; }
        public StoragePlace StoragePlace { get; set; }
    }
}
