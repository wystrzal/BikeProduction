using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Core.Models;

namespace Warehouse.Application.Mapping
{
    public class GetPartDto
    {
        public int Id { get; set; }
        public string PartName { get; set; }
        public string Reference { get; set; }
        public int Quantity { get; set; }
        public ICollection<ProductsParts> ProductsParts { get; set; }
        public int StoragePlaceId { get; set; }
        public string StoragePlaceName { get; set; }
    }
}
