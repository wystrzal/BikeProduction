using Microsoft.AspNetCore.Mvc.Rendering;
using ShopMVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static ShopMVC.Models.Enums.HomeProductEnum;

namespace ShopMVC.Interfaces
{
    public interface ICatalogService
    {
        Task<List<CatalogProduct>> GetProducts(CatalogFilteringData filteringData);
        Task<IEnumerable<SelectListItem>> GetBrandListItem();
        Task<List<CatalogProduct>> GetHomeProducts(HomeProduct homeProduct);
        Task<CatalogProduct> GetProduct(int id);
    }
}
