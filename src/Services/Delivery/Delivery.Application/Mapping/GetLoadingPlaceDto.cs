using Delivery.Core.Models;
using System.Collections.Generic;
using static Delivery.Core.Models.Enums.LoadingPlaceStatusEnum;

namespace Delivery.Application.Mapping
{
    public class GetLoadingPlaceDto
    {
        public int Id { get; set; }
        public string LoadingPlaceName { get; set; }
        public int LoadedQuantity { get; set; }
        public int AmountOfSpace { get; set; }
        public LoadingPlaceStatus LoadingPlaceStatus { get; set; }
        public virtual ICollection<PackToDelivery> PacksToDelivery { get; set; }
    }
}
