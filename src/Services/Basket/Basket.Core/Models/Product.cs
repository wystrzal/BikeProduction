using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Basket.Core.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Required]
        public int Price { get; set; }

        [Required]
        public string Reference { get; set; }

        public int Quantity { get; set; }
    }
}
