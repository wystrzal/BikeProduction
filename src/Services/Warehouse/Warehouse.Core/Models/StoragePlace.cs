using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.Core.Models
{
    public class StoragePlace
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Part Part { get; set; }
    }
}
