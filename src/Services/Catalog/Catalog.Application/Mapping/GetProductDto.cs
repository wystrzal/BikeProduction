using System;
using System.Collections.Generic;
using System.Text;
using static Catalog.Core.Models.Enums.BikeTypeEnum;
using static Catalog.Core.Models.Enums.ColorsEnum;

namespace Catalog.Application.Mapping
{
    public class GetProductDto
    {
        public string ProductName { get; set; }
        public Colors Colors { get; set; }
        public decimal Price { get; set; }
        public string PhotoUrl { get; set; }
        public BikeType BikeType { get; set; }
        public string BrandName { get; set; }
    }
}
