﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.Application.Mapping
{
    public class GetPartsDto
    {
        public string PartName { get; set; }
        public string Reference { get; set; }
        public int Quantity { get; set; }
    }
}