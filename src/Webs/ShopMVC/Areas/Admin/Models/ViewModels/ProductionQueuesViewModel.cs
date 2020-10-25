using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ShopMVC.Areas.Admin.Models.Enums.ProductionStatusEnum;

namespace ShopMVC.Areas.Admin.Models.ViewModels
{
    public class ProductionQueuesViewModel
    {
        public List<ProductionQueue> ProductionQueues { get; set; }
        public ProductionStatus ProductionStatus { get; set; }
    }
}
