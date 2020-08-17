using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMVC.Models.ViewModels
{
    public class HomePageViewModel
    {
        public List<CatalogProduct> NewProducts { get; set; }
        public List<CatalogProduct> PopularProducts { get; set; }
    }
}
