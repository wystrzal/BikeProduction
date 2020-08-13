using Microsoft.AspNetCore.Mvc.Rendering;
using ShopMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMVC.Interfaces
{
    public interface ICatalogService
    {
        Task<List<CatalogProduct>> GetProducts(FilteringData filteringData);
        Task<IEnumerable<SelectListItem>> GetBrandListItem();
    }
}
