﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMVC.Areas.Admin.Models.ViewModels
{
    public class PartsViewModel
    {
        public List<Part> Parts { get; set; }
        public string Reference { get; set; }
        public int ProductId { get; set; }
    }
}
