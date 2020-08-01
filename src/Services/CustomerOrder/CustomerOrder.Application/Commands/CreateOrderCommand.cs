using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerOrder.Core.Models;
using MediatR;

namespace CustomerOrder.Application.Commands
{
    public class CreateOrderCommand : IRequest
    {
        public string UserId { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
