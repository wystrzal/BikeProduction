using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMVC.Models.ViewModels
{
    public class CatalogProductsViewModel
    {
        public List<CatalogProduct> CatalogProducts { get; set; } = new List<CatalogProduct>();
        public int Take { get; set; }
    }
}
