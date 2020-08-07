using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopMVC.Models
{
    public class CatalogProduct
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Reference { get; set; }
        public decimal Price { get; set; }
        public string PhotoUrl { get; set; }
    }
}
