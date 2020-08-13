﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMVC.Models.ViewModels
{
    public class CatalogProductsViewModel
    {
        public List<CatalogProduct> CatalogProducts { get; set; } = new List<CatalogProduct>();
        public FilteringData FilteringData { get; set; }
        public IEnumerable<SelectListItem> BrandListItem { get; set; }
    }
}

