using Microsoft.AspNetCore.Mvc.Rendering;
using ShopMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMVC.Areas.Admin.Models.ViewModels
{
    public class CreateProductViewModel
    {
        public CatalogProduct Product { get; set; }
        public IEnumerable<SelectListItem> Brand { get; set; }
    }
}
