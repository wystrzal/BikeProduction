using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.Mapping
{
    public class GetProductsDto
    {
        public string ProductName { get; set; }
        public string Reference { get; set; }
        public decimal Price { get; set; }
        public string PhotoUrl { get; set; }
    }
}
