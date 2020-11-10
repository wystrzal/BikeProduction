using System.Collections.Generic;
using static ShopMVC.Areas.Admin.Models.Enums.PackStatusEnum;

namespace ShopMVC.Areas.Admin.Models.ViewModels
{
    public class PacksViewModel
    {
        public PackStatus PackStatus { get; set; }
        public List<PackToDelivery> Packs { get; set; }
    }
}
