using System.ComponentModel.DataAnnotations;
using static ShopMVC.Models.Enums.BikeTypeEnum;
using static ShopMVC.Models.Enums.ColorsEnum;

namespace ShopMVC.Models
{
    public class CatalogProduct
    {
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public string Reference { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The field Price must be greater than zero.")]
        public decimal Price { get; set; }

        [Required]
        public string PhotoUrl { get; set; }

        [Required]
        public Colors Colors { get; set; }

        [Required]
        public BikeType BikeType { get; set; }

        public string BrandName { get; set; }

        [Required]
        public int BrandId { get; set; }
    }
}
