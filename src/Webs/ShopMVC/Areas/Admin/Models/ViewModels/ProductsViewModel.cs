using ShopMVC.Models;
using System.Collections.Generic;

namespace ShopMVC.Areas.Admin.Models.ViewModels
{
    public class ProductsViewModel
    {
        public List<CatalogProduct> Products { get; set; }
    }
}
