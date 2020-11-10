using System.Collections.Generic;

namespace ShopMVC.Areas.Admin.Models.ViewModels
{
    public class PartsViewModel
    {
        public List<Part> Parts { get; set; }
        public string Reference { get; set; }
        public int ProductId { get; set; }
    }
}
