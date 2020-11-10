using Microsoft.AspNetCore.Mvc.Rendering;
using ShopMVC.Models;
using System.Collections.Generic;

namespace ShopMVC.Areas.Admin.Models.ViewModels
{
    public class PostPutProductViewModel
    {
        public CatalogProduct Product { get; set; }
        public IEnumerable<SelectListItem> Brand { get; set; }
        public List<Part> Parts { get; set; }
    }
}
