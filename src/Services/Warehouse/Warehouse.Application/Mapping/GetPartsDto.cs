﻿namespace Warehouse.Application.Mapping
{
    public class GetPartsDto
    {
        public int Id { get; set; }
        public string PartName { get; set; }
        public string Reference { get; set; }
        public int Quantity { get; set; }
    }
}
