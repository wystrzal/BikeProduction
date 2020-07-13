using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerOrder.Application.Mapping
{
    public class GetOrderDto
    {
        public DateTime OrderDate { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
