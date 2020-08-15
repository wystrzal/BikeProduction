using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static Catalog.Core.Models.ColorsEnum;

namespace Catalog.Core.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public string Reference { get; set; }

        [Required]
        public Colors Colors { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Required]
        public decimal Price { get; set; }
        
        [Required]
        public string PhotoUrl { get; set; }

        [Required]
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public DateTime DateAdded { get; set; }

        public Product()
        {
            DateAdded = DateTime.Now;
        }
    }
}
