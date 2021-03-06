﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Catalog.Core.Models.Enums.BikeTypeEnum;
using static Catalog.Core.Models.Enums.ColorsEnum;

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

        [Required]
        public BikeType BikeType { get; set; }

        [Required]
        public Brand Brand { get; set; }
        public int Popularity { get; set; }
        public DateTime DateAdded { get; set; }

        public Product()
        {
            DateAdded = DateTime.Now;
        }
    }
}
