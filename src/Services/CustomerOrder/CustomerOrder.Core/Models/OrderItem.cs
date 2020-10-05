using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerOrder.Core.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Reference { get; set; }

        public string PhotoUrl { get; set; }
        public int Quantity { get; set; }

        [JsonIgnore]
        public Order Order { get; set; }
    }
}

