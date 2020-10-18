using System;
using System.Collections.Generic;
using System.Text;
using static Delivery.Core.Models.Enums.PackStatusEnum;

namespace Delivery.Application.Mapping
{
    public class GetPackDto
    {
        public int Id { get; set; }
        public int ProductsQuantity { get; set; }
        public PackStatus PackStatus { get; set; }
        public int OrderId { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int LoadingPlaceId { get; set; }
        public DateTime Date { get; set; }
    }
}
