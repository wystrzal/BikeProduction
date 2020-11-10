using System;
using static Delivery.Core.Models.Enums.PackStatusEnum;

namespace Delivery.Application.Mapping
{
    public class GetPacksDto
    {
        public int Id { get; set; }
        public int ProductsQuantity { get; set; }
        public PackStatus PackStatus { get; set; }
        public DateTime Date { get; set; }
    }
}
