﻿using static Catalog.Core.Models.Enums.BikeTypeEnum;
using static Catalog.Core.Models.Enums.ColorsEnum;

namespace Catalog.Application.Mapping
{
    public class GetProductDto
    {
        public int Id { get; set; }
        public string Reference { get; set; }
        public string ProductName { get; set; }
        public Colors Colors { get; set; }
        public decimal Price { get; set; }
        public string PhotoUrl { get; set; }
        public BikeType BikeType { get; set; }
        public string BrandName { get; set; }
    }
}
