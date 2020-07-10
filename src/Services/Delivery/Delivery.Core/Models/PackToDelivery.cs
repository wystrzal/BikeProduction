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
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
        public LoadingPlace LoadingPlace { get; set; }
    }
}
