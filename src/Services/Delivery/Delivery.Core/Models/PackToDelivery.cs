using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Delivery.Core.Models
{
    public class PackToDelivery
    {
        public int Id { get; set; }
        public int ProductsQuantity { get; set; }
        public int OrderId { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int LoadingPlaceId { get; set; }
        public LoadingPlace LoadingPlace { get; set; }
    }
}
