using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ShopMVC.Models.ViewModels
{
    public class CatalogProductsViewModel
    {
        public List<CatalogProduct> CatalogProducts { get; set; } = new List<CatalogProduct>();
        public FilteringData FilteringData { get; set; }
        public IEnumerable<SelectListItem> BrandListItem { get; set; }
    }
}

