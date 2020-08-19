using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static ShopMVC.Models.Enums.BikeTypeEnum;
using static ShopMVC.Models.Enums.ColorsEnum;

namespace ShopMVC.Models
{
    public class CatalogProduct
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Reference { get; set; }
        public decimal Price { get; set; }
        public string PhotoUrl { get; set; }
        public Colors Colors { get; set; }
        public BikeType BikeType { get; set; }
        public string BrandName { get; set; }
    }
}
