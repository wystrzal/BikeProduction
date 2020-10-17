using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ShopMVC.Areas.Admin.Models.Enums.PackStatusEnum;

namespace ShopMVC.Areas.Admin.Models.ViewModels
{
    public class DeliveryViewModel
    {
        public PackStatus PackStatus { get; set; }
        public List<PackToDelivery> Packs { get; set; }
    }
}
