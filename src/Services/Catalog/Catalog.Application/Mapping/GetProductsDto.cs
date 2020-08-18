﻿namespace Catalog.Application.Mapping
{
    public class GetProductsDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Reference { get; set; }
        public decimal Price { get; set; }
        public string PhotoUrl { get; set; }
    }
}
