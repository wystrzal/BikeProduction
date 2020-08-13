﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Core.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
