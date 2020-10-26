using CustomerOrder.Core.Models;
using MediatR;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomerOrder.Application.Commands
{
    public class CreateOrderCommand : IRequest
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string CustomerFirstName { get; set; }

        [Required]
        public string CustomerLastName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string PostCode { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string HouseNumber { get; set; }

        [Range(0, int.MaxValue)]
        public decimal TotalPrice { get; set; }

        [Required]
        public List<OrderItem> OrderItems { get; set; }
    }
}
