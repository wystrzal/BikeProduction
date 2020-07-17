using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.Application.Mapping
{
    public class GetStoragePlacesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool ItsFree { get; set; }
    }
}
