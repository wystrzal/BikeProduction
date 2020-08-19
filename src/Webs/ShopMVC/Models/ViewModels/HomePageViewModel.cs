using System.Collections.Generic;

namespace ShopMVC.Models.ViewModels
{
    public class HomePageViewModel
    {
        public List<CatalogProduct> NewProducts { get; set; }
        public List<CatalogProduct> PopularProducts { get; set; }
    }
}
