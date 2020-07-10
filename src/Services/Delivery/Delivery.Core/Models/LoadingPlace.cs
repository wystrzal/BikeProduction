using Delivery.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using static Delivery.Core.Models.Enums.LoadingPlaceStatusEnum;

namespace Delivery.Core.Models
{
    public class LoadingPlace
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public LoadingPlaceStatus LoadingPlaceStatus { get; set; }
        public int LoadingPlaceNumber { get; set; }
        public ICollection<PackToDelivery> PacksToDelivery { get; set; }
    }
}
