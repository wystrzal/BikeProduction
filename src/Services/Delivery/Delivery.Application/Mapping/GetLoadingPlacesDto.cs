using System;
using System.Collections.Generic;
using System.Text;
using static Delivery.Core.Models.Enums.LoadingPlaceStatusEnum;

namespace Delivery.Application.Mapping
{
    public class GetLoadingPlacesDto
    {
        public int Id { get; set; }
        public string LoadingPlaceName { get; set; }
        public int LoadedQuantity { get; set; }
        public int AmountOfSpace { get; set; }
        public LoadingPlaceStatus LoadingPlaceStatus { get; set; }
    }
}
