using System;
using System.Collections.Generic;
using System.Text;
using static Delivery.Core.Models.Enums.LoadingPlaceStatusEnum;

namespace Delivery.Core.SearchSpecification
{
    public class LoadingPlaceFilteringData
    {
        public LoadingPlaceStatus LoadingPlaceStatus { get; set; }
    }
}
