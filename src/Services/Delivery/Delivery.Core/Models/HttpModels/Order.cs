using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Delivery.Core.Models
{
    public class Order
    {
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
