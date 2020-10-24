using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ShopMVC.Areas.Admin.Models.Enums.ProductionStatusEnum;

namespace ShopMVC.Areas.Admin.Models
{
    public class ProductionQueue
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Reference { get; set; }
        public int Quantity { get; set; }
        public ProductionStatus ProductionStatus { get; set; }
    }
}
