using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerOrder.Core.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Required]
        public int ProductPrice { get; set; }

        [Required]
        public string Reference { get; set; }

        public int Quantity { get; set; }

        [JsonIgnore]
        public Order Order { get; set; }
    }
}
