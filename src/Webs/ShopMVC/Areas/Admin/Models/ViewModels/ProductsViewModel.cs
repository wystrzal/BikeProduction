using ShopMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMVC.Areas.Admin.Models.ViewModels
{
    public class ProductsViewModel
    {
        public List<CatalogProduct> Products { get; set; }
    }
}
