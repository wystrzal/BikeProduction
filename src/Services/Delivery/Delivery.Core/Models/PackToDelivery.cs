using System;
using static Delivery.Core.Models.Enums.PackStatusEnum;

namespace Delivery.Core.Models
{
    public class PackToDelivery
    {
        public int Id { get; set; }
        public int ProductsQuantity { get; set; }
        public PackStatus PackStatus { get; set; }
        public int OrderId { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public LoadingPlace LoadingPlace { get; set; }
        public DateTime Date { get; set; } = new DateTime();
    }
}
