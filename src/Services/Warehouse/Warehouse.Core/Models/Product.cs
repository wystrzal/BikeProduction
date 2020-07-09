using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Core.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Reference { get; set; }
        public int Quantity { get; set; }
    }
}
