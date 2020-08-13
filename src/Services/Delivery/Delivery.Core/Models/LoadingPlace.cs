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
        public int LoadedQuantity { get; set; }
        public int AmountOfSpace { get; set; }
        public LoadingPlaceStatus LoadingPlaceStatus { get; set; }
        public int LoadingPlaceNumber { get; set; }
        public virtual ICollection<PackToDelivery> PacksToDelivery { get; set; }
    }
}
