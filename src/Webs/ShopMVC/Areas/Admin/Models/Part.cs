using System;
using System.ComponentModel.DataAnnotations;

namespace ShopMVC.Areas.Admin.Models
{
    public class Part
    {
        public int Id { get; set; }

        [Required]
        public string PartName { get; set; }

        [Required]
        public string Reference { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The field QuantityForProduction must be greater than zero.")]
        public int QuantityForProduction { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The field Quantity must be greater than zero.")]
        public int Quantity { get; set; }
    }
}
