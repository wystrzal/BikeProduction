using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ShopMVC.Areas.Admin.Models.Enums.PackStatusEnum;

namespace ShopMVC.Areas.Admin.Models
{
    public class PackToDelivery
    {
        public int Id { get; set; }
        public int ProductsQuantity { get; set; }
        public PackStatus PackStatus { get; set; }
        public int OrderId { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int LoadingPlaceId { get; set; }
        public DateTime Date { get; set; }
    }
}
